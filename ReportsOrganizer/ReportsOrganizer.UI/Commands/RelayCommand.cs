using ReportsOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReportsOrganizer.UI.Command
{
    //class RelayCommand : ICommand
    //{
    //    private List<WeakReference> _canExecuteChangedHandlers;

    //    private Action<object> action;
    //    private bool canExecute;

    //    public RelayCommand(Action<object> action, bool canExecute)
    //    {
    //        this.action = action;
    //        this.canExecute = canExecute;
    //    }

    //    public void RaiseCanExecuteChanged()
    //    {
    //        CanExecuteChanged?.Invoke(this, new EventArgs());
    //    }

    //    public event EventHandler CanExecuteChanged = delegate { };

    //    public virtual bool CanExecute(object parameter)
    //    {
    //        return canExecute;
    //    }

    //    public virtual void Execute(object parameter)
    //    {
    //        action(parameter);
    //    }

    //    /// <summary>Raises the CanExecuteChanged event.</summary>
    //    public virtual void InvalidateCanExecute()
    //    {
    //        WeakEventHandlerManager.CallWeakReferenceHandlers(this, _canExecuteChangedHandlers);
    //    }
    //}

    public class RelayCommand : ICommand
    {
        private List<WeakReference> _canExecuteChangedHandlers;

        protected readonly Action<object> ExecuteAction;
        protected readonly Predicate<object> СanExecutePredicate;

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                WeakEventHandlerManager.AddWeakReferenceHandler(ref _canExecuteChangedHandlers, value);
            }
            remove
            {
                WeakEventHandlerManager.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value);
            }
        }

        public RelayCommand(Action<object> execute) : this(execute, null)
        {

        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            ExecuteAction = execute;
            СanExecutePredicate = canExecute;
        }

        public RelayCommand(Action<object> execute, bool canExecute)
        {
            ExecuteAction = execute;
            СanExecutePredicate = (e)=> { return true; };
        }

        /// <summary>Raises the CanExecuteChanged event.</summary>
        public virtual void InvalidateCanExecute()
        {
            WeakEventHandlerManager.CallWeakReferenceHandlers(this, _canExecuteChangedHandlers);
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>
        /// True if this command can be executed, otherwise - false.
        /// </returns>
        public virtual bool CanExecute(object parameter)
        {
            if (СanExecutePredicate != null)
                return СanExecutePredicate(parameter);
            return true;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public virtual void Execute(object parameter)
        {
            ExecuteAction(parameter);
        }
    }

    class AsyncCommand : RelayCommand
    {
        private readonly Func<object, Task> _execute;
        private bool _isExecuting;

        public AsyncCommand(Func<object, Task> execute) : this(execute, null)
        {

        }

        public AsyncCommand(Func<object, Task> execute, Predicate<object> canExecute) : base(null, canExecute)
        {
            _execute = execute;
        }

        public override bool CanExecute(object parameter)
        {
            return !_isExecuting && base.CanExecute(parameter);
        }

        public override async void Execute(object parameter)
        {
            _isExecuting = true;
            InvalidateCanExecute();
            try
            {
                await _execute(parameter);
            }
            finally
            {
                _isExecuting = false;
                InvalidateCanExecute();
            }
        }

        protected virtual void OnCanExecuteChanged()
        {
            InvalidateCanExecute();
        }
    }
}

