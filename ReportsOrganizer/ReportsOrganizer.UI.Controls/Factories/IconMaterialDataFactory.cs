using ReportsOrganizer.UI.Controls.Enums;
using System.Collections.Generic;

namespace ReportsOrganizer.UI.Controls.Factories
{
    public static class IconMaterialDataFactory
    {
        internal static IDictionary<IconMaterialKind, string> Create()
        {
            return new Dictionary<IconMaterialKind, string> {
                { IconMaterialKind.None, "" },
                { IconMaterialKind.ArrowRight, "M8.59,16.58L13.17,12L8.59,7.41L10,6L16,12L10,18L8.59,16.58Z" },
            };
        }
    }
}
