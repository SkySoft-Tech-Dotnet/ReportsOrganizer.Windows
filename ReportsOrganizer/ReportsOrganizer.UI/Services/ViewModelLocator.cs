using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.UI.ViewModels.Settings;
using ReportsOrganizer.UI.ViewModels.Windows;

namespace ReportsOrganizer.UI.Services
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindow =>
            ServiceCollectionProvider.Container.GetInstance<MainWindowViewModel>();

        public ManageProjectsWindowViewModel ManageProjectsWindow =>
            ServiceCollectionProvider.Container.GetInstance<ManageProjectsWindowViewModel>();

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
    }
}
