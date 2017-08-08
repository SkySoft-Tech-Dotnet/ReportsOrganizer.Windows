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
    }

    public class HomeViewModel : BaseViewModel, IHomeViewModel
    {
        private INavigationService navigation;

        public HomeViewModel(INavigationService navigationService)
        {
            navigation = navigationService;
        }
    }
}
