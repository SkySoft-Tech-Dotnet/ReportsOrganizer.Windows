using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReportsOrganizer.UI.Command;

namespace ReportsOrganizer.UI.Models
{
    public static class CollectionExtensions
    {
        public static BindingList<IntItem> ToBindingList(this IEnumerable<int> items)
        {
            return new BindingList<IntItem>(items.Select(e => new IntItem(e)).ToList());
        }
    }

    public class IntItem : INotifyPropertyChanged
    {
        private int _number;

        public IntItem(int value)
        {
            _number = value;
        }

        public int Number
        {
            get => _number;
            set
            {
                if (_number != value)
                {
                    _number = value;
                    NotifyPropertyChanged(nameof(Number));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
