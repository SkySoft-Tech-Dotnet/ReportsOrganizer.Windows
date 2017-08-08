using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportsOrganizer.UI.ViewModel;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Services;
using System.ComponentModel;

namespace ReportsOrganizer.UI.ViewModels
{
    public interface IHomeViewModel
    {
        ICommand SettingsCommand { get; }
    }

    public class HomeViewModel : BaseViewModel, IHomeViewModel
    {
        private INavigationService navigation;

        public ICommand SettingsCommand { get; private set; }

        public HomeViewModel(INavigationService navigationService)
        {
            navigation = navigationService;
            SettingsCommand = new RelayCommand(SettingsAction, true);
        }

        private void SettingsAction(object sender)
        {
            navigation.NavigateToSettings();
        }
    }
}
