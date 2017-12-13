using ReportsOrganizer.UI.Enums;
using ReportsOrganizer.UI.Factories;
using System.Windows;

namespace ReportsOrganizer.UI.Controls
{
    public class PackIconMaterial : PackIcon<PackIconMaterialKind>
    {
        static PackIconMaterial()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PackIconMaterial),
                new FrameworkPropertyMetadata(typeof(PackIconMaterial)));
        }

        public PackIconMaterial() : base(PackIconMaterialDataFactory.Create)
        {
        }
    }
}
