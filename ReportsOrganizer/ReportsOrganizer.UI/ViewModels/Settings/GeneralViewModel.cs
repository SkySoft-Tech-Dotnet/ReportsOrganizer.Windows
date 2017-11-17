using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using WPFLocalizeExtension.Engine;

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

        public Dictionary<string, string> Languages
            => new Dictionary<string, string>
            {
                { "en", "English" },
                { "ru", "Русский" },
                { "uk", "Українська" }
            };

        public KeyValuePair<string, string> SelectedLanguage
        {
            get => Languages.FirstOrDefault(language
                => language.Key == LocalizeDictionary.Instance.Culture.Name);
            set
            {
                _applicationSettings.Value.General.Language = value.Key;
                LocalizeDictionary.Instance.Culture = new CultureInfo(value.Key);
                NotifyPropertyChanged(nameof(SelectedLanguage));
                _applicationSettings.UpdateAsync(default(CancellationToken));
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
