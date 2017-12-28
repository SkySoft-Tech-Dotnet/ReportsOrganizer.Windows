using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ReportsOrganizer.Core.Services;

namespace ReportsOrganizer.UI.Managers
{
    public sealed class CrossThreadManager
    {
        private const string OpenEventName = "F4313BD4-32AB-48C6-9790-682F3B48C022";
        private const string NotificationEventName = "87A300C0-FFBC-42FE-A357-AE9DE1EAB5AE";
        private const string SingletonMutexName = "CA13A683-04A7-41E5-BFB1-43D22BADADB7";

        private CancellationToken _cancellationToken;

        private static bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());

        private readonly Mutex _singletonMutex;

        private static Lazy<CrossThreadManager> _instance = new Lazy<CrossThreadManager>(() => new CrossThreadManager());

        public static CrossThreadManager Instance => _instance.Value;
        

        public event EventHandler ActivateApplication;

        public event EventHandler ActiveInstanceDetected;

        public event EventHandler ShowNotifyForm;
        
        private CrossThreadManager()
        {
            _singletonMutex = new Mutex(true, SingletonMutexName);
            _cancellationToken = new CancellationToken();
        }

        public void HandleApplicationStart(string[] startupArguments)
        {
            WaitForEvents();

            if (startupArguments.Contains(TaskService.NotificationKey))
            {
                InvokeEventWaitHandle(NotificationEventName);
                OnActiveInstanceDetected();
            }

            if (!IsInDesignMode && !_singletonMutex.WaitOne(TimeSpan.Zero, true))
            {
                InvokeEventWaitHandle(OpenEventName);
                OnActiveInstanceDetected();
            }
        }
        
        private void WaitForEvents()
        {
            Task.Factory.StartNew(() =>
            {
                var handle = new EventWaitHandle(false, EventResetMode.AutoReset, OpenEventName);
                while (!_cancellationToken.IsCancellationRequested)
                {
                    handle.WaitOne();

                    if (!Application.Current.Dispatcher.CheckAccess() && !_cancellationToken.IsCancellationRequested)
                        Application.Current.Dispatcher.Invoke(OnActivateApplication);
                }
            }, _cancellationToken);

            Task.Factory.StartNew(() =>
            {
                var handle = new EventWaitHandle(false, EventResetMode.AutoReset, NotificationEventName);
                while (!_cancellationToken.IsCancellationRequested)
                {
                    handle.WaitOne();

                    if (!Application.Current.Dispatcher.CheckAccess() && !_cancellationToken.IsCancellationRequested)
                        Application.Current.Dispatcher.Invoke(OnShowNotifyForm);
                }
            }, _cancellationToken);
        }

        private void InvokeEventWaitHandle(string name)
        {
            if (EventWaitHandle.TryOpenExisting(name, out var eventHandle))
            {
                eventHandle.Set();
            }
        }
        
        private void OnActivateApplication()
        {
            ActivateApplication?.Invoke(this, EventArgs.Empty);
        }

        private void OnShowNotifyForm()
        {
            ShowNotifyForm?.Invoke(this, EventArgs.Empty);
        }

        private void OnActiveInstanceDetected()
        {
            ActiveInstanceDetected?.Invoke(this, EventArgs.Empty);
        }
    }
}
