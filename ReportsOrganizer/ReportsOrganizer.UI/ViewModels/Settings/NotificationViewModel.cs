using System;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using System.Windows.Input;
using ReportsOrganizer.Core.Managers;
using ReportsOrganizer.Core.Services.ScheduleServices;
using ReportsOrganizer.UI.Command;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class NotificationViewModel : BaseViewModel
    {
        private INotificationManager _notificationManager;

        public CultureInfo DurationCultureInfo { get; }

        public ObservableCollection<SelectedTime> CustomTimes { get; }


        public TimeSpan SelectedInterval
        {
            get => _notificationManager.GetService<IntervalScheduleService>().GetPeriod();
            set
            {
                _notificationManager.GetService<IntervalScheduleService>().AddInterval(value);
                NotifyPropertyChanged(nameof(SelectedInterval));
            }
        }

        public ICommand AddCustomTimeCommand { get; }
        public ICommand RemoveCustomTimeCommand { get; }


        public NotificationViewModel(INotificationManager notificationManager)
        {
            _notificationManager = notificationManager;

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
            var time = TimeSpan.FromHours(1);
            _notificationManager.GetService<DailyScheduleService>().AddTask(time);
            _notificationManager.GetService<IntervalScheduleService>().AddInterval(time);
            
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
