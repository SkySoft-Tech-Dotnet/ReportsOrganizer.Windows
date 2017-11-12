using ReportsOrganizer.UI.Abstractions;
using System.ComponentModel;

namespace ReportsOrganizer.UI.Internal
{
    internal class ApplicationSettingsManager : INotifyPropertyChanged
    {
        private BaseViewModel _currentPage;
        private BaseViewModel _previousPage;

        public BaseViewModel CurrentPage
        {
            get => _currentPage;
            set => SetValue(ref _currentPage, value, nameof(CurrentPage));
        }

        public BaseViewModel PreviousPage
        {
            get => _previousPage;
            set => SetValue(ref _previousPage, value, nameof(PreviousPage));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SetValue<T>(ref T field, T value, string propertyName)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
