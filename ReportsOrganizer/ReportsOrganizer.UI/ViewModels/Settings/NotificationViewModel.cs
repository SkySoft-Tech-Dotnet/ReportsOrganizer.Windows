using System;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows.Input;
using ReportsOrganizer.UI.Command;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class NotificationViewModel : BaseViewModel
    {
        private IApplicationOptions<ApplicationSettings> _applicationOptions;

        private ApplicationSettings ApplicationSettings => _applicationOptions.Value;

        public CultureInfo DurationCultureInfo { get; }

        public ObservableCollection<SelectedTime> CustomTimes { get; }

        public bool EnableInterval
        {
            get => ApplicationSettings.Notification.EnableInterval;
            set
            {
                ApplicationSettings.Notification.EnableInterval = value;
                NotifyPropertyChanged(nameof(EnableInterval));
                _applicationOptions.UpdateAsync(default(CancellationToken));
            }
        }

        public bool EnableAtTime
        {
            get => ApplicationSettings.Notification.EnableAtTime;
            set
            {
                ApplicationSettings.Notification.EnableAtTime = value;
                NotifyPropertyChanged(nameof(EnableAtTime));
                _applicationOptions.UpdateAsync(default(CancellationToken));
            }
        }

        public bool EnableIgnoreTime
        {
            get => ApplicationSettings.Notification.EnableIgnoreTime;
            set
            {
                ApplicationSettings.Notification.EnableIgnoreTime = value;
                NotifyPropertyChanged(nameof(EnableIgnoreTime));
                _applicationOptions.UpdateAsync(default(CancellationToken));
            }
        }

        public ApplicationNotification Interval
        {
            get => ApplicationSettings.Notification.Interval;
            set
            {
                ApplicationSettings.Notification.Interval = value;
                NotifyPropertyChanged(nameof(Interval));
                _applicationOptions.UpdateAsync(default(CancellationToken));
            }
        }

        public IEnumerable<ApplicationNotification> AtTimes
        {
            get => ApplicationSettings.Notification.AtTimes;
            set
            {
                ApplicationSettings.Notification.AtTimes = value;
                NotifyPropertyChanged(nameof(AtTimes));
                _applicationOptions.UpdateAsync(default(CancellationToken));
            }
        }

        public IEnumerable<ApplicationNotificationInterval> IgnoreTimes
        {
            get => ApplicationSettings.Notification.IgnoreTimes;
            set
            {
                ApplicationSettings.Notification.IgnoreTimes = value;
                NotifyPropertyChanged(nameof(IgnoreTimes));
                _applicationOptions.UpdateAsync(default(CancellationToken));
            }
        }

        public ICommand AddCustomTimeCommand { get; }
        public ICommand RemoveCustomTimeCommand { get; }


        public NotificationViewModel(IApplicationOptions<ApplicationSettings> applicationOptions)
        {
            _applicationOptions = applicationOptions;

            DurationCultureInfo = new CultureInfo("uk-UA")
            {
                DateTimeFormat =
                {
                    ShortTimePattern = "HH:mm",
                    LongTimePattern = "HH:mm"
                }
            };

            AddCustomTimeCommand = new RelayCommand(AddCustomTimeAction, true);
            RemoveCustomTimeCommand = new RelayCommand(RemoveCustomTimeAction, true);

            CustomTimes = new ObservableCollection<SelectedTime>
            {
                new SelectedTime{ Value = TimeSpan.FromMinutes(122)},
                new SelectedTime{ Value = TimeSpan.FromMinutes(226)},
                new SelectedTime{ Value = TimeSpan.FromMinutes(527)}
            };
        }

        private void AddCustomTimeAction(object obj)
        {
            CustomTimes.Add(new SelectedTime { Value = TimeSpan.FromMinutes(0) });
            NotifyPropertyChanged(nameof(CustomTimes));
        }

        private void RemoveCustomTimeAction(object obj)
        {
            CustomTimes.Remove((SelectedTime)obj);
            NotifyPropertyChanged(nameof(CustomTimes));
        }
    }
}
