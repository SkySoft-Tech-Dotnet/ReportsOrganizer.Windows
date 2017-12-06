using MahApps.Metro;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Abstractions;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Models;
using SimpleInjector;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace ReportsOrganizer.UI.ViewModels.Settings
{
    public class PersonalizationViewModel : BaseViewModel
    {
        private Container _container;
        private IApplicationOptions<ApplicationSettings> _applicationSettings;

        public ICommand ChooseThemeClickCommand { get; } 

        public PersonalizationViewModel(
            Container container,
            IApplicationOptions<ApplicationSettings> applicationSettings)
        {
            _container = container;
            _applicationSettings = applicationSettings;

            ChooseThemeClickCommand = new RelayCommand(ChooseThemeAction, true);
        }

        private void ChooseThemeAction(object obj)
        {
            _applicationSettings.Value.Personalization.Theme = (string)obj;
            _applicationSettings.UpdateAsync(default(CancellationToken));

            var theme = _applicationSettings.Value.Personalization.Theme == "Default" ?
                ThemeManager.GetAppTheme("DefaultTheme") :
                ThemeManager.GetAppTheme("BaseLight");

            ThemeManager.ChangeAppStyle(
                Application.Current,
                ThemeManager.GetAccent(_applicationSettings.Value.Personalization.Theme),
                ThemeManager.DetectAppStyle(Application.Current).Item1);
        }
    }
}
