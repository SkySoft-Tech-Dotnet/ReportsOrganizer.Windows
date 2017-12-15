using System;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class NotificationViewModel : BaseViewModel
    {
        private IApplicationOptions<ApplicationSettings> _applicationOptions;

        private ApplicationSettings ApplicationSettings => _applicationOptions.Value;

        public CultureInfo DurationCultureInfo { get; }

        public List<TimeSpan> CustomTimes { get; }

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

            CustomTimes = new List<TimeSpan>
            {
                TimeSpan.FromHours(2),
                TimeSpan.FromHours(4)
            };
        }
    }
}
