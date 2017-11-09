using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Services;
using ReportsOrganizer.Core.Services;
using System.ComponentModel;
using ReportsOrganizer.Models;
using System.Windows.Data;
using System.Globalization;
using System.Runtime.CompilerServices;
using ReportsOrganizer.UI.Annotations;
using ReportsOrganizer.UI.Models;

namespace ReportsOrganizer.UI.ViewModels
{
    public interface ISettingsViewModel
    {
        IList<int> ListHours { get; }
        IList<int> ListMinutes { get; }
    }


    public class SettingsViewModel : BaseViewModel, ISettingsViewModel
    {
        private INavigationService _navigationService;
        private IConfigurationService<ApplicationSettings> _configurationService;
        private IScheduleService _scheduleService;
        private BindingList<IntItem> _specificTimes;

        public ICommand AddAnotherTimeCommand { get; private set; }
        public ICommand RemoveTimeCommand { get; private set; }

        public IList<int> ListHours { get; }
        public IList<int> ListMinutes { get; }


        public bool StartupWithWindows
        {
            get => _configurationService.Value.General.StartupWithWindows;
            set
            {
                if (!value)
                    StartMinimized = false;
                _configurationService.Value.General.StartupWithWindows = value;
                NotifyPropertyChanged(nameof(StartupWithWindows));
            }
        }
        public bool StartMinimized
        {
            get => _configurationService.Value.General.StartMinimized;
            set
            {
                _configurationService.Value.General.StartMinimized = value;
                NotifyPropertyChanged(nameof(StartMinimized));
            }
        }


        public int IntervalHours
        {
            get => _configurationService.Value.Notification.Interval / 3600;
            set
            {
                _configurationService.Value.Notification.Interval =
                    _configurationService.Value.Notification.Interval + (value - IntervalHours) * 3600;
                NotifyPropertyChanged(nameof(IntervalHours));
            }
        }
        public int IntervalMinutes
        {
            get => _configurationService.Value.Notification.Interval % 3600 / 60;
            set
            {
                _configurationService.Value.Notification.Interval =
                    _configurationService.Value.Notification.Interval + (value - IntervalMinutes) * 60;
                NotifyPropertyChanged(nameof(IntervalMinutes));
            }
        }
        public BindingList<IntItem> SpecificTimes => _specificTimes;

        public SettingsViewModel(
            INavigationService navigationService,
            IConfigurationService<ApplicationSettings> configurationSettings,
            IScheduleService scheduleService)
        {
            _navigationService = navigationService;
            _configurationService = configurationSettings;
            _scheduleService = scheduleService;
            _specificTimes = _configurationService.Value.Notification.Times.ToBindingList();

            AddAnotherTimeCommand = new RelayCommand(AddAnotherTimeAction, true);
            RemoveTimeCommand = new RelayCommand(RemoveTimeAction, true);

            ListHours = new List<int>(Enumerable.Range(0, 24));
            ListMinutes = new List<int>(Enumerable.Range(0, 60));

            PropertyChanged += UpdateSettings;


            _specificTimes.ListChanged += BindingListChanged;
        }

        public void AddAnotherTimeAction(object sender)
        {
            _specificTimes.Add(new IntItem(0));
        }
        public void RemoveTimeAction(object sender)
        {
            _specificTimes.Remove(sender as IntItem);
        }

        private void BindingListChanged(object sender, ListChangedEventArgs e)
        {
            _configurationService.Value.Notification.Times = (sender as BindingList<IntItem>)?.Select(x => x.Number);
            Task.Run(async () => await _configurationService.UpdateAsync());
        }

        private void UpdateSettings(object sender, PropertyChangedEventArgs e)
        {
            Task.Run(async () => await _configurationService.UpdateAsync());
        }
    }
}