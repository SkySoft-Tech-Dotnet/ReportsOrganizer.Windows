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
using System.Timers;
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

        public bool IntervalEnabled
        {
            get => _configurationService.Value.Notification.IntervalEnabled;
            set
            {
                _scheduleService.IsIntervalEnabled = value;
                _configurationService.Value.Notification.IntervalEnabled = value;
                NotifyPropertyChanged(nameof(IntervalEnabled));
            }
        }
        public bool TimesEnabled
        {
            get => _configurationService.Value.Notification.TimesEnabled;
            set
            {
                _configurationService.Value.Notification.TimesEnabled = value;
                NotifyPropertyChanged(nameof(TimesEnabled));
            }
        }
        public int IntervalHours
        {
            get => _configurationService.Value.Notification.Interval / 60;
            set
            {
                _configurationService.Value.Notification.Interval =
                    _configurationService.Value.Notification.Interval + (value - IntervalHours) * 60;
                NotifyPropertyChanged(nameof(IntervalHours));
            }
        }
        public int IntervalMinutes
        {
            get => _configurationService.Value.Notification.Interval % 60;
            set
            {
                _configurationService.Value.Notification.Interval =
                    _configurationService.Value.Notification.Interval + (value - IntervalMinutes);
                NotifyPropertyChanged(nameof(IntervalMinutes));
            }
        }
        public BindingList<IntItem> SpecificTimes { get; }

        public SettingsViewModel(
            INavigationService navigationService,
            IConfigurationService<ApplicationSettings> configurationSettings,
            IScheduleService scheduleService)
        {
            _navigationService = navigationService;
            _configurationService = configurationSettings;
            _scheduleService = scheduleService;

            _scheduleService.Action = _navigationService.ShowNotificationWindow;

            AddAnotherTimeCommand = new RelayCommand(AddAnotherTimeAction, true);
            RemoveTimeCommand = new RelayCommand(RemoveTimeAction, true);

            ListHours = new List<int>(Enumerable.Range(0, 24));
            ListMinutes = new List<int>(Enumerable.Range(0, 60));

            SpecificTimes = _configurationService.Value.Notification.Times.ToBindingList();

            PropertyChanged += UpdateSettings;
            SpecificTimes.ListChanged += BindingListChanged;
        }

        public void AddAnotherTimeAction(object sender)
        {
            SpecificTimes.Add(new IntItem(0));
        }
        public void RemoveTimeAction(object sender)
        {
            SpecificTimes.Remove(sender as IntItem);
        }

        private void BindingListChanged(object sender, ListChangedEventArgs e)
        {
            _configurationService.Value.Notification.Times = (sender as BindingList<IntItem>)?.Select(x => x.Number);
            _scheduleService.RefreshDailyTimers();
            Task.Run(async () => await _configurationService.UpdateAsync());
        }

        private void UpdateSettings(object sender, PropertyChangedEventArgs e)
        {
            _scheduleService.RefreshFixedTimer();
            _scheduleService.RefreshDailyTimers();
            Task.Run(async () => await _configurationService.UpdateAsync());
        }
    }
}