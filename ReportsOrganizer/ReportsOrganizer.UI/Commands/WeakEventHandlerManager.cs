using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ReportsOrganizer.UI.Command
{
    internal static class WeakEventHandlerManager
    {
        public static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler)
        {
            if (handlers == null)
                handlers = new List<WeakReference>();
            handlers.Add(new WeakReference(handler));
        }

        public static void RemoveWeakReferenceHandler(List<WeakReference> handlers, EventHandler handler)
        {
            if (handlers == null)
                return;
            for (int index = handlers.Count - 1; index >= 0; --index)
            {
                var target = handlers[index].Target as EventHandler;
                if (target == null || target == handler)
                    handlers.RemoveAt(index);
            }
        }

        public static void CallWeakReferenceHandlers(object sender, List<WeakReference> handlers)
        {
            if (handlers == null)
                return;
            EventHandler[] callees = new EventHandler[handlers.Count];
            int count = 0;
            int num = CleanupOldHandlers(handlers, callees, count);
            for (int index = 0; index < num; ++index)
                CallHandler(sender, callees[index]);
        }

        private static void CallHandler(object sender, EventHandler eventHandler)
        {
            if (eventHandler == null)
                return;
            var currentDispatcher = Dispatcher.CurrentDispatcher;
            if (!currentDispatcher.CheckAccess())
                currentDispatcher.BeginInvoke(new Action<object, EventHandler>(CallHandler), sender, eventHandler as object);
            else
                eventHandler(sender, EventArgs.Empty);
        }

        private static int CleanupOldHandlers(List<WeakReference> handlers, EventHandler[] callees, int count)
        {
            for (int index = handlers.Count - 1; index >= 0; --index)
            {
                WeakReference handler = handlers[index];
                EventHandler target = handler.Target as EventHandler;
                if (target == null || !handler.IsAlive)
                {
                    handlers.RemoveAt(index);
                }
                else
                {
                    callees[count] = target;
                    ++count;
                }
            }
            return count;
        }
    }
}
