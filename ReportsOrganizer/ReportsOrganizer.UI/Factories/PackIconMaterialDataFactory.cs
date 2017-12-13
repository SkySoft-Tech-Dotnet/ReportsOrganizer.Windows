using ReportsOrganizer.UI.Enums;
using System.Collections.Generic;

namespace ReportsOrganizer.UI.Factories
{
    internal static class PackIconMaterialDataFactory
    {
        internal static IDictionary<PackIconMaterialKind, string> Create()
        {
            return new Dictionary<PackIconMaterialKind, string> {
                { PackIconMaterialKind.None, "" },
                { PackIconMaterialKind.ArrowLeft, "M20,11V13H8L13.5,18.5L12.08,19.92L4.16,12L12.08,4.08L13.5,5.5L8,11H20Z" },
            };
        }
    }
}
