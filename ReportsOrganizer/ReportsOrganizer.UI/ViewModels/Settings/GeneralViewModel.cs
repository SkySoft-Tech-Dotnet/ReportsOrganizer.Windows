using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Models;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class GeneralViewModel : BaseViewModel
    {
        private IApplicationOptions<ApplicationSettings> _applicationSettings;
        private IApplicationManage _applicationManage;

        private bool _enabledAutorun;
        public bool EnabledAutorun
        {
            get => _enabledAutorun;
            set
            {
                if (!value)
                {
                    _enabledRunMinimized = value;
                    NotifyPropertyChanged(nameof(EnabledRunMinimized));
                }
                _enabledAutorun = value;
                _applicationManage.ChangeAutorun(value);
                NotifyPropertyChanged(nameof(EnabledAutorun));
            }
        }

        private bool _enabledRunMinimized;
        public bool EnabledRunMinimized
        {
            get => _enabledRunMinimized;
            set
            {
                _enabledRunMinimized = value;
                _applicationManage.ChangeAutorunMinimize(value);
                NotifyPropertyChanged(nameof(EnabledRunMinimized));
            }
        }

        public GeneralViewModel(
            IApplicationOptions<ApplicationSettings> applicationSettings,
            IApplicationManage applicationManage)
        {
            _applicationSettings = applicationSettings;
            _applicationManage = applicationManage;

            _enabledAutorun = _applicationManage.IsAutorun;
            _enabledRunMinimized = _applicationManage.IsAutorunMinimize;
        }
    }
}
