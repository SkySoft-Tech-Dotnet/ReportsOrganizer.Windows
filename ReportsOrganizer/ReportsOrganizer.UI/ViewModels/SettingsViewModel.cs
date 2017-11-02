using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Services;

namespace ReportsOrganizer.UI.ViewModels
{
    public interface ISettingsViewModel
    {
        ICommand BackCommand { get; }
        bool StartupWithWindows { get; set; }

        IList<int> ListHours { get; }
        IList<int> ListMinutes { get; }
    }

    public class SettingsViewModel : BaseViewModel, ISettingsViewModel
    {
        private INavigationService navigation;

        public ICommand BackCommand { get; private set; }
        public IList<int> ListHours { get; }
        public IList<int> ListMinutes { get; }

        private bool _startMinimized;
        public bool StartMinimized
        {
            get
            {
                return _startMinimized;
            }
            set
            {
                _startMinimized = value;
                NotifyPropertyChanged(nameof(StartMinimized));
            }
        }

        private bool _startupWithWindows;
        public bool StartupWithWindows
        {
            get
            {
                return _startupWithWindows;
            }
            set
            {
                _startupWithWindows = value;
                if (!_startupWithWindows)
                    StartMinimized = _startupWithWindows;
                NotifyPropertyChanged(nameof(StartupWithWindows));
            }
        }

        public SettingsViewModel(INavigationService navigationService)
        {
            navigation = navigationService;
            BackCommand = new RelayCommand(BackAction, true);

            ListHours = new List<int>(Enumerable.Range(0, 24));
            ListMinutes = new List<int>(Enumerable.Range(0, 59));
        }

        private void BackAction(object sender)
        {
            navigation.NavigateToHome();
        }
    }
}





