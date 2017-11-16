using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Models;
using System.Threading;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class GeneralViewModel : BaseViewModel
    {
        private IApplicationOptions<ApplicationSettings> _applicationSettings;
        private IApplicationManage _applicationManage;

        private bool _enableAutorun;
        public bool EnabledAutorun
        {
            get => _applicationManage.IsAutorun;
            set
            {
                if (!(_enableAutorun = value))
                {
                    _applicationSettings.Value.General.StartMinimized = false;
                    _applicationSettings.UpdateAsync(default(CancellationToken));
                    NotifyPropertyChanged(nameof(EnabledRunMinimized));
                }
                _applicationManage.IsAutorun = value;
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

        public GeneralViewModel(
            IApplicationOptions<ApplicationSettings> applicationSettings,
            IApplicationManage applicationManage)
        {
            _applicationSettings = applicationSettings;
            _applicationManage = applicationManage;
        }
    }
}
