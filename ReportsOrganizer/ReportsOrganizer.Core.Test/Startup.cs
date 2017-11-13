using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportsOrganizer.Core.Extensions;
using ReportsOrganizer.Core.Infrastructure;
using ReportsOrganizer.UI.Models;

namespace ReportsOrganizer.Core.Test
{
    [TestClass()]
    public class Startup
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            IoC.Container.AddConfiguration<ApplicationSettings>("appsettings.test.json");
        }
    }
}
