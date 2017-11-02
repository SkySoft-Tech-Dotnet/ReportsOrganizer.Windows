using ReportsOrganizer.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReportsOrganizer.UI.ViewModels
{
    public interface IHomeViewModel
    {
    }

    public class HomeViewModel : BaseViewModel, IHomeViewModel
    {
        private INavigationService navigation;

        public IEnumerable<int> YearEntry { get; }

        public HomeViewModel(INavigationService navigationService)
        {
            navigation = navigationService;

            YearEntry = Enumerable.Range(2015, DateTime.Today.Year - 2014)
                .ToList().AsReadOnly();
        }
    }
}
