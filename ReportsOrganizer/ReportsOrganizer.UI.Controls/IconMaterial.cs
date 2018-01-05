using ReportsOrganizer.UI.Controls.Enums;
using ReportsOrganizer.UI.Controls.Factories;
using System.Windows;

namespace ReportsOrganizer.UI.Controls
{
    public class IconMaterial : Icon<IconMaterialKind>
    {
        static IconMaterial()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IconMaterial),
                new FrameworkPropertyMetadata(typeof(IconMaterial)));
        }

        public IconMaterial() : base(IconMaterialDataFactory.Create)
        {
        }
    }
}
