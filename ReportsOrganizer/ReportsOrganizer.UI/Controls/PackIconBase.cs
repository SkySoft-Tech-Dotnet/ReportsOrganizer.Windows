using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ReportsOrganizer.UI.Controls
{
    public abstract class PackIconBase : Control
    {
        internal abstract void UpdateData();
    }

    public abstract class PackIconBase<TKind> : PackIconBase
    {
        private static Lazy<IDictionary<TKind, string>> _dataIndex;

        protected PackIconBase(Func<IDictionary<TKind, string>> dataIndexFactory)
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
            = DependencyProperty.Register(nameof(Kind), typeof(TKind), typeof(PackIconBase<TKind>),
                new PropertyMetadata(default(TKind), KindPropertyChangedCallback));

        private static void KindPropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((PackIconBase)dependencyObject).UpdateData();
        }

        public TKind Kind
        {
            get => (TKind)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }

        private static readonly DependencyPropertyKey DataPropertyKey
            = DependencyProperty.RegisterReadOnly(nameof(Data), typeof(string), typeof(PackIconBase<TKind>),
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
