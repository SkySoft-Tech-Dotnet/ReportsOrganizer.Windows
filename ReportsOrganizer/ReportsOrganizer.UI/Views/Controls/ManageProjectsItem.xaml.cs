using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReportsOrganizer.UI.Views.Controls
{
    /// <summary>
    /// Interaction logic for ManageProjectsItem.xaml
    /// </summary>
    public partial class ManageProjectsItem
    {
        public static readonly DependencyProperty ShortNameProperty =
            DependencyProperty.Register("ShortName", typeof(object),
                typeof(ManageProjectsItem), new PropertyMetadata(null));

        public static readonly DependencyProperty FullNameProperty =
            DependencyProperty.Register("FullName", typeof(object),
                typeof(ManageProjectsItem), new PropertyMetadata(null));

        public static readonly DependencyProperty ActivateProjectCommandProperty =
            DependencyProperty.Register("ActivateProjectCommand", typeof(ICommand),
                typeof(ManageProjectsItem), new PropertyMetadata(null));

        public static readonly DependencyProperty CheckBoxVisibilityProperty =
            DependencyProperty.Register("CheckBoxVisibility", typeof(object),
                typeof(ManageProjectsItem), new PropertyMetadata(null));

        public static readonly DependencyProperty IsProjectActiveProperty =
            DependencyProperty.Register("IsProjectActive", typeof(object),
                typeof(ManageProjectsItem), new PropertyMetadata(null));

        public static readonly DependencyProperty EditProjectCommandProperty =
            DependencyProperty.Register("EditProjectCommand", typeof(ICommand),
                typeof(ManageProjectsItem), new PropertyMetadata(null));

        public static readonly DependencyProperty DeleteProjectCommandProperty =
            DependencyProperty.Register("DeleteProjectCommand", typeof(ICommand),
                typeof(ManageProjectsItem), new PropertyMetadata(null));

        public static readonly DependencyProperty ProjectCommandParameterProperty =
            DependencyProperty.Register("ProjectCommandParameter", typeof(object),
                typeof(ManageProjectsItem), new PropertyMetadata(null));

        public ManageProjectsItem()
        {
            InitializeComponent();
            MainGrid.DataContext = this;
        }

        public object ShortName
        {
            get => GetValue(ShortNameProperty);
            set => SetValue(ShortNameProperty, value);
        }

        public object FullName
        {
            get => GetValue(FullNameProperty);
            set => SetValue(FullNameProperty, value);
        }

        public ICommand ActivateProjectCommand
        {
            get => (ICommand)GetValue(ActivateProjectCommandProperty);
            set => SetValue(ActivateProjectCommandProperty, value);
        }

        public object CheckBoxVisibility
        {
            get => GetValue(CheckBoxVisibilityProperty);
            set => SetValue(CheckBoxVisibilityProperty, value);
        }

        public object IsProjectActive
        {
            get => GetValue(IsProjectActiveProperty);
            set => SetValue(IsProjectActiveProperty, value);
        }

        public ICommand EditProjectCommand
        {
            get => (ICommand)GetValue(EditProjectCommandProperty);
            set => SetValue(EditProjectCommandProperty, value);
        }

        public ICommand DeleteProjectCommand
        {
            get => (ICommand)GetValue(DeleteProjectCommandProperty);
            set => SetValue(DeleteProjectCommandProperty, value);
        }

        public object ProjectCommandParameter
        {
            get => GetValue(ProjectCommandParameterProperty);
            set => SetValue(ProjectCommandParameterProperty, value);
        }
    }
}
