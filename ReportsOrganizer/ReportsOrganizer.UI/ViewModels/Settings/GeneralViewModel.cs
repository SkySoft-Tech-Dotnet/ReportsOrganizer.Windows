using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Models;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class GeneralViewModel : BaseViewModel
    {
        private IApplicationOptions<ApplicationSettings> _applicationSettings;
        private IApplicationManage _applicationManage;
        
        public bool EnabledAutorun
        {
            get => _applicationManage.IsAutorun;
            set
            {
                _applicationManage.ChangeAutorun(value);
                NotifyPropertyChanged(nameof(EnabledAutorun));
                NotifyPropertyChanged(nameof(EnabledRunMinimized));
            }
        }

        public bool EnabledRunMinimized
        {
            get => _applicationManage.IsAutorunMinimize;
            set
            {
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
        }
    }
}
