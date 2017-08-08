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
    }

    public class SettingsViewModel : BaseViewModel, ISettingsViewModel
    {
        private INavigationService navigation;

        public ICommand BackCommand { get; private set; }

        public SettingsViewModel(INavigationService navigationService)
        {
            navigation = navigationService;
            BackCommand = new RelayCommand(BackAction, true);
        }

        private void BackAction(object sender)
        {
            navigation.NavigateToHome();
        }
    }
}
