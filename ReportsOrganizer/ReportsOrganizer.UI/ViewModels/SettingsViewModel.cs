using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Services;
using ReportsOrganizer.UI.Models;
using ReportsOrganizer.Core.Services;
using System.ComponentModel;

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
        private INavigationService _navigationService;
        private IConfigurationService<ApplicationSettings> _configurationService;

        public ICommand BackCommand { get; private set; }
        public IList<int> ListHours { get; }
        public IList<int> ListMinutes { get; }
        
        public bool StartMinimized
        {
            get => _configurationService.Value.General.StartMinimized;
            set
            {
                _configurationService.Value.General.StartMinimized = value;
                NotifyPropertyChanged(nameof(StartMinimized));
            }
        }
        
        public bool StartupWithWindows
        {
            get
            {
                return _configurationService.Value.General.StartupWithWindows;
            }
            set
            {
                if (!value)
                    StartMinimized = value;
                _configurationService.Value.General.StartupWithWindows = value;
                NotifyPropertyChanged(nameof(StartupWithWindows));
            }
        }

        public SettingsViewModel(INavigationService navigationService, IConfigurationService<ApplicationSettings> configurationSettings)
        {
            _navigationService = navigationService;
            _configurationService = configurationSettings;

            BackCommand = new RelayCommand(BackAction, true);

            ListHours = new List<int>(Enumerable.Range(0, 24));
            ListMinutes = new List<int>(Enumerable.Range(0, 59));

            PropertyChanged += UpdateSettings;
        }

        private void BackAction(object sender)
        {
            _navigationService.NavigateToHome();
        }

        private void UpdateSettings(object sender, PropertyChangedEventArgs e)
        {
            Task.Run(async () => await _configurationService.UpdateAsync());
            //await _configurationService.UpdateAsync();
        }
    }
}





