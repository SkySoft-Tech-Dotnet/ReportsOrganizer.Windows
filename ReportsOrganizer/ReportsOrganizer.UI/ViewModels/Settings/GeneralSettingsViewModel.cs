using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Models;
using System.Threading;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class GeneralSettingsViewModel : BaseViewModel
    {
        private IApplicationOptions<ApplicationSettings> _applicationSettings;

        private bool _enableAutorun;
        public bool EnabledAutorun
        {
            get => _enableAutorun;
            set
            {
                if (!(_enableAutorun = value))
                {
                    _applicationSettings.Value.General.StartMinimized = false;
                    _applicationSettings.UpdateAsync(default(CancellationToken));
                    NotifyPropertyChanged(nameof(EnabledRunMinimized));
                }
                NotifyPropertyChanged(nameof(EnabledAutorun));
            }
        }

        public bool EnabledRunMinimized
        {
            get => _applicationSettings.Value.General.StartMinimized;
            set
            {
                _applicationSettings.Value.General.StartMinimized = value;
                _applicationSettings.UpdateAsync(default(CancellationToken));
                NotifyPropertyChanged(nameof(EnabledRunMinimized));
            }
        }

        public GeneralSettingsViewModel(IApplicationOptions<ApplicationSettings> applicationSettings)
            => _applicationSettings = applicationSettings;
    }
}
