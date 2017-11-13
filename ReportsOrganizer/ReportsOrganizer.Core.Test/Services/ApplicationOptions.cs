using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DI.Providers;
using ReportsOrganizer.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Test.Services
{
    [TestClass]
    public class ApplicationOptions
    {
        [DataTestMethod]
        [DataRow("ru"), DataRow("ua"), DataRow("en")]
        public async Task UpdateAsync(string language)
        {
            var service = ServiceCollectionProvider.Container
                .GetInstance<IApplicationOptions<ApplicationSettings>>();

            service.Value.General.Language = language;
            await service.UpdateAsync(default(CancellationToken));

            var settings = JObject.Parse(File.ReadAllText("appsettings.test.json"));
            Assert.IsTrue(settings["general"]?.Value<string>("language") == language);
        }

        [DataTestMethod]
        [DataRow("ru", 5), DataRow("ua", 10), DataRow("en", 50)]
        public async Task UpdateAsyncThreads(string language, int taskCount)
        {
            var service = ServiceCollectionProvider.Container
                .GetInstance<IApplicationOptions<ApplicationSettings>>();
            service.Value.General.Language = language;

            var tasks = new List<Task>();
            for (int i = 0; i < taskCount; i++)
            {
                tasks.Add(service.UpdateAsync(default(CancellationToken)));
            }
            await Task.WhenAll(tasks);

            var settings = JObject.Parse(File.ReadAllText("appsettings.test.json"));
            Assert.IsTrue(settings["general"]?.Value<string>("language") == language);
        }

        [DataTestMethod]
        [DataRow("ru", 5), DataRow("ua", 10), DataRow("en", 50)]
        public async Task UpdateAsyncThreadsDelay(string language, int taskCount)
        {
            var service = ServiceCollectionProvider.Container
                .GetInstance<IApplicationOptions<ApplicationSettings>>();
            service.Value.General.Language = language;

            var tasks = new List<Task>();
            var random = new Random();

            for (int i = 0; i < taskCount; i++)
            {
                tasks.Add(UpdateAsyncThreadsDelay(service, random.Next(0, 1000)));
            }
            await Task.WhenAll(tasks);

            var settings = JObject.Parse(File.ReadAllText("appsettings.test.json"));
            Assert.IsTrue(settings["general"]?.Value<string>("language") == language);
        }

        private async Task UpdateAsyncThreadsDelay(
            IApplicationOptions<ApplicationSettings> service, int delay)
        {
            await Task.Delay(delay);
            await service.UpdateAsync(default(CancellationToken));
        }
    }
}
