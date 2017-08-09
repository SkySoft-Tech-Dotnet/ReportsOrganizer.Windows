using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Services;

namespace ReportsOrganizer.UI.ViewModels
{
    public interface ISettingsViewModel
    {
        ICommand BackCommand { get; }

        IList<int> ListHours { get; }
        IList<int> ListMinutes { get; }
    }

    public class SettingsViewModel : BaseViewModel, ISettingsViewModel
    {
        private INavigationService navigation;

        public ICommand BackCommand { get; private set; }
        public IList<int> ListHours { get; }
        public IList<int> ListMinutes { get; }

        public SettingsViewModel(INavigationService navigationService)
        {
            navigation = navigationService;
            BackCommand = new RelayCommand(BackAction, true);

            ListHours = new List<int>(Enumerable.Range(0, 24));
            ListMinutes = new List<int>(Enumerable.Range(0, 59));
        }

        private void BackAction(object sender)
        {
            navigation.NavigateToHome();
        }
    }
}
