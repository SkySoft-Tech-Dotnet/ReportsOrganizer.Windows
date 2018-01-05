using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ReportsOrganizer.UI.Controls.Abstractions
{
    public abstract class IconBase : Control
    {
        internal abstract void UpdateData();
    }

    public abstract class IconBase<TKind> : IconBase
    {
        private static Lazy<IDictionary<TKind, string>> _dataIndex;

        protected IconBase(Func<IDictionary<TKind, string>> dataIndexFactory)
        {
            if (dataIndexFactory == null)
            {
                throw new ArgumentNullException(nameof(dataIndexFactory));
            }
            if (_dataIndex == null)
            {
                _dataIndex = new Lazy<IDictionary<TKind, string>>(dataIndexFactory);
            }
        }

        public static readonly DependencyProperty KindProperty
            = DependencyProperty.Register("Kind", typeof(TKind), typeof(IconBase<TKind>),
                new PropertyMetadata(default(TKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((IconBase)dependencyObject).UpdateData();
        }

        public TKind Kind
        {
            get => (TKind)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        private static readonly DependencyPropertyKey DataPropertyKey
            = DependencyProperty.RegisterReadOnly("Data", typeof(string), typeof(IconBase<TKind>),
                new PropertyMetadata(string.Empty));

        [TypeConverter(typeof(GeometryConverter))]
        public string Data
        {
            get => (string)GetValue(DataPropertyKey.DependencyProperty);
            private set => SetValue(DataPropertyKey, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateData();
        }

        internal override void UpdateData()
        {
            string data = null;
            _dataIndex.Value?.TryGetValue(Kind, out data);
            Data = data;
        }
    }
}
