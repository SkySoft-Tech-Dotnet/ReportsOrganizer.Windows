using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using ReportsOrganizer.Core.Infrastructure;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.UI.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Test.Services
{
    [TestClass]
    public class ApplicationOptions
    {
        [DataTestMethod, DisplayName("ApplicationOptions:UpdateAsync")]
        [DataRow("ru"), DataRow("ua"), DataRow("en")]
        public async Task UpdateAsync(string language)
        {
            var service = IoC.Container.GetInstance<IApplicationOptions<ApplicationSettings>>();

            service.Value.General.Language = language;
            await service.UpdateAsync(default(CancellationToken));

            var settings = JObject.Parse(File.ReadAllText("appsettings.test.json"));
            Assert.IsTrue(settings["general"]?.Value<string>("language") == language);
        }

        [DataTestMethod, DisplayName("ApplicationOptions:UpdateAsync:Threads")]
        [DataRow("ru"), DataRow("ua"), DataRow("en")]
        public async Task UpdateAsyncThreads(string language)
        {
            var service = IoC.Container.GetInstance<IApplicationOptions<ApplicationSettings>>();
            service.Value.General.Language = language;

            var tasks = new List<Task>();
            for (int i = 0; i < 50; i++)
            {
                tasks.Add(service.UpdateAsync(default(CancellationToken)));
            }
            await Task.WhenAll(tasks);

            var settings = JObject.Parse(File.ReadAllText("appsettings.test.json"));
            Assert.IsTrue(settings["general"]?.Value<string>("language") == language);
        }
    }
}
