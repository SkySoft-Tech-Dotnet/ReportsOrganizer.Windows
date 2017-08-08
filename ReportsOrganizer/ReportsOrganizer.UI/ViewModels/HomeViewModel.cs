using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using ReportsOrganizer.UI.Command;
using ReportsOrganizer.UI.Services;

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
