using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Models;
using System.Collections.Generic;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class NotificationViewModel : BaseViewModel
    {
        private IApplicationOptions<ApplicationSettings> _applicationOptions;

        private ApplicationSettings ApplicationSettings
        {
            get => _applicationOptions.Value;
        }

        public bool EnableInterval
        {
            get => ApplicationSettings.Notification.EnableInterval;
            set
            {
                ApplicationSettings.Notification.EnableInterval = value;
                NotifyPropertyChanged(nameof(EnableInterval));
            }
        }

        public bool EnableAtTime
        {
            get => ApplicationSettings.Notification.EnableAtTime;
            set
            {
                ApplicationSettings.Notification.EnableAtTime = value;
                NotifyPropertyChanged(nameof(EnableAtTime));
            }
        }

        public bool EnableIgnoreTime
        {
            get => ApplicationSettings.Notification.EnableIgnoreTime;
            set
            {
                ApplicationSettings.Notification.EnableIgnoreTime = value;
                NotifyPropertyChanged(nameof(EnableIgnoreTime));
            }
        }

        public ApplicationNotification Interval
        {
            get => ApplicationSettings.Notification.Interval;
            set
            {
                ApplicationSettings.Notification.Interval = value;
                NotifyPropertyChanged(nameof(Interval));
            }
        }

        public IEnumerable<ApplicationNotification> AtTimes
        {
            get => ApplicationSettings.Notification.AtTimes;
            set
            {
                ApplicationSettings.Notification.AtTimes = value;
                NotifyPropertyChanged(nameof(AtTimes));
            }
        }

        public IEnumerable<ApplicationNotificationInterval> IgnoreTimes
        {
            get => ApplicationSettings.Notification.IgnoreTimes;
            set
            {
                ApplicationSettings.Notification.IgnoreTimes = value;
                NotifyPropertyChanged(nameof(IgnoreTimes));
            }
        }

        public NotificationViewModel(IApplicationOptions<ApplicationSettings> applicationOptions)
            => _applicationOptions = applicationOptions;
    }
}
