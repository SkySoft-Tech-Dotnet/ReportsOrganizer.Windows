using ReportsOrganizer.UI.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ReportsOrganizer.UI.DependencyProperties
{
    public static class WindowProperties
    {
        public static readonly DependencyProperty BoundClosing =
            DependencyProperty.RegisterAttached("BoundClosing", typeof(ICommand), typeof(WindowProperties), new PropertyMetadata(null, OnWindowClosing));

        private static readonly DependencyProperty BindClosing =
            DependencyProperty.RegisterAttached("BindClosing", typeof(bool), typeof(WindowProperties), new PropertyMetadata(false));

        private static void OnWindowClosing(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            Window window = dp as Window;

            if (dp == null || GetBoundClosing(dp) == null)
            {
                return;
            }

            window.Closing += HandleWindowClosing;
        }

        private static void HandleWindowClosing(object sender, CancelEventArgs e)
        {
            ICommand command = GetBoundClosing((DependencyObject)sender);
            command.Execute(sender);

            if (GetBindClosing((DependencyObject)sender))
            {
                e.Cancel = true;
            }
        }

        public static void SetBoundClosing(DependencyObject dp, ICommand value)
        {
            dp.SetValue(BoundClosing, value);
        }

        public static ICommand GetBoundClosing(DependencyObject dp)
        {
            return (ICommand)dp.GetValue(BoundClosing);
        }

        public static void SetBindClosing(DependencyObject dp, bool value)
        {
            dp.SetValue(BindClosing, value);
        }

        public static bool GetBindClosing(DependencyObject dp)
        {
            return (bool)dp.GetValue(BindClosing);
        }
    }
}