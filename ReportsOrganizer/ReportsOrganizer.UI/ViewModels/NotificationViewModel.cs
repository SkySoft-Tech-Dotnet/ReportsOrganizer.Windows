using ReportsOrganizer.UI.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ReportsOrganizer.UI.ViewModel
{
    public class NotificationViewModel : BaseViewModel
    {
        public ICommand NotificationWindowUsePreviousCommand { get; private set; }
        public ICommand NotificationWindowPostponrCommand { get; private set; }
        public ICommand NotificationWindowOKCommand { get; private set; }


        public NotificationViewModel()
        {
            NotificationWindowUsePreviousCommand = new RelayCommand(NotificationWindowUsePreviousAction, true);
            NotificationWindowPostponrCommand = new RelayCommand(NotificationWindowPostponrAction, true);
            NotificationWindowOKCommand = new RelayCommand(NotificationWindowOKAction, true);
                                    
        }

        private void NotificationWindowUsePreviousAction(object sender)
        {

        }

        private void NotificationWindowPostponrAction(object sender)
        {

        }

        private void NotificationWindowOKAction(object sender)
        {

        }
    }
}
