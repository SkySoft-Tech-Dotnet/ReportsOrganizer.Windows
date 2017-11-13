using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportsOrganizer.Core.Extensions;
using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.UI.Models;

namespace ReportsOrganizer.Core.Test
{
    [TestClass]
    public class Startup
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            ServiceCollectionProvider.Container
                .AddConfiguration<ApplicationSettings>("appsettings.test.json");
        }
    }
}
