using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.UI.ViewModels.Settings;
using ReportsOrganizer.UI.ViewModels.Windows;

namespace ReportsOrganizer.UI.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindow =>
            ServiceCollectionProvider.Container.GetInstance<MainWindowViewModel>();

        public GeneralSettingsViewModel GeneralSettings =>
            ServiceCollectionProvider.Container.GetInstance<GeneralSettingsViewModel>();





        public MainViewModel Main
        {
            get { return null; }
            //get { return IoC.Container.GetInstance<MainViewModel>(); }
        }

        public NotificationViewModel Notification
        {
            get { return null; }
            //get { return IoC.Container.GetInstance<NotificationViewModel>(); }
        }

        public HomeViewModel HomePage
        {
            get { return null; }
            //get { return IoC.Container.GetInstance<HomeViewModel>(); }
        }

        //public SettingsViewModel SettingsPage
        //{
        //    get { return IoC.Container.GetInstance<SettingsViewModel>(); }
        //}
    }
}
