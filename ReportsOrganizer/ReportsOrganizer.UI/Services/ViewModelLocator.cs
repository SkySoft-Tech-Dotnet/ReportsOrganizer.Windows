using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.UI.ViewModels.Settings;
using ReportsOrganizer.UI.ViewModels.Windows;

namespace ReportsOrganizer.UI.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindow =>
            ServiceCollectionProvider.Container.GetInstance<MainWindowViewModel>();

        public ManageProjectWindowViewModel ManageProjectWindow =>
            ServiceCollectionProvider.Container.GetInstance<ManageProjectWindowViewModel>();

        public NotificationWindowViewModel NotificationWindow =>
            ServiceCollectionProvider.Container.GetInstance<NotificationWindowViewModel>();

        public GeneralViewModel GeneralSettings =>
            ServiceCollectionProvider.Container.GetInstance<GeneralViewModel>();

        public ManageProjectsViewModel ManageProjectsSettings =>
            ServiceCollectionProvider.Container.GetInstance<ManageProjectsViewModel>();

        public NotificationViewModel NotificationSettings =>
            ServiceCollectionProvider.Container.GetInstance<NotificationViewModel>();

        public PersonalizationViewModel PersonalizationSettings =>
            ServiceCollectionProvider.Container.GetInstance<PersonalizationViewModel>();

        





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

        //public SettingsViewModel SettingsPage
        //{
        //    get { return IoC.Container.GetInstance<SettingsViewModel>(); }
        //}
    }
}
