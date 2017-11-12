using ReportsOrganizer.UI.Abstractions;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class GeneralSettingsViewModel : BaseViewModel
    {
        private bool _enableAutorun;
        private bool _enableMinimized;

        public bool EnabledAutorun
        {
            get => _enableAutorun;
            set
            {
                if (!(_enableAutorun = value))
                {
                    _enableMinimized = false;
                    NotifyPropertyChanged(nameof(EnabledRunMinimized));
                }
                NotifyPropertyChanged(nameof(EnabledAutorun));
            }
        }

        public bool EnabledRunMinimized
        {
            get => _enableMinimized;
            set => SetValue(ref _enableMinimized, value, nameof(EnabledRunMinimized));
        }
    }
}
