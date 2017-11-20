using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReportsOrganizer.Core.Managers;
using ReportsOrganizer.Core.Services;
using ReportsOrganizer.DI.Providers;
using System;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Test.Managers
{
    [TestClass]
    public class SchedulerManage
    {
        [DataTestMethod]
        [DataRow(200, 300, 400)]
        [DataRow(500, 400, 1000)]
        public async Task SchedulerManage_JobRunAt(int timeoutFirstJob, int timeoutSecondJob, int timeout)
        {
            var service = ServiceCollectionProvider.Container.GetInstance<ISchedulerManage>();
            service.EnabledJobsWithSpecificTime = true;

            bool runnedFirstJob = false;
            bool runnedSecondJob = false;

            await WpfContext.Run(async () =>
            {
                var firstJob = new JobService(() =>
                {
                    runnedFirstJob = true;
                    return Task.CompletedTask;
                }).ToRunAt(DateTime.Now.AddMilliseconds(timeoutFirstJob).TimeOfDay);

                var secondJob = new JobService(() =>
                {
                    runnedSecondJob = true;
                    return Task.CompletedTask;
                }).ToRunAt(DateTime.Now.AddMilliseconds(timeoutSecondJob).TimeOfDay);

                service
                    .AddJob(firstJob)
                    .AddJob(secondJob);

                await Task.Delay(timeout);

                Assert.IsTrue(Math.Abs((DateTime.Now.AddDays(1) - firstJob.NextRun).TotalSeconds) < 1);
                Assert.IsTrue(Math.Abs((DateTime.Now.AddDays(1) - secondJob.NextRun).TotalSeconds) < 1);
            });

            Assert.IsTrue(runnedFirstJob);
            Assert.IsTrue(runnedSecondJob);
        }

        [DataTestMethod]
        [DataRow(100, 500)]
        public async Task SchedulerManage_JobRunOnceAt(int timeoutJob, int timeout)
        {
            var service = ServiceCollectionProvider.Container.GetInstance<ISchedulerManage>();
            service.EnabledJobsWithSpecificTime = true;

            await WpfContext.Run(async () =>
            {
                bool runnedJob = false;
                service.AddJob(new JobService(() =>
                {
                    if (runnedJob)
                    {
                        Assert.Fail("Second start...");
                    }
                    runnedJob = true;
                    return Task.CompletedTask;
                }).ToRunOnceAt(DateTime.Now.AddMilliseconds(timeoutJob).TimeOfDay));

                await Task.Delay(timeout);
                Assert.IsTrue(runnedJob);
            });
        }

        [DataTestMethod]
        [DataRow(100, 500)]
        public async Task SchedulerManage_JobDisable(int timeoutJob, int timeout)
        {
            var service = ServiceCollectionProvider.Container.GetInstance<ISchedulerManage>();
            service.EnabledJobsWithSpecificTime = true;

            await WpfContext.Run(async () =>
            {
                var job = new JobService(() =>
                {
                    Assert.Fail("Started job...");
                    return Task.CompletedTask;
                }).ToRunAt(DateTime.Now.AddMilliseconds(timeoutJob).TimeOfDay);

                job.Enabled = false;
                service.AddJob(job);

                await Task.Delay(timeout);
            });
        }

        [DataTestMethod]
        [DataRow(250, 600, 2)]
        public async Task SchedulerManage_JobRunEvery(int timeoutJob, int timeout, int result)
        {
            var service = ServiceCollectionProvider.Container.GetInstance<ISchedulerManage>();
            service.EnabledRepeatJobs = true;

            var countRun = 0;
            await WpfContext.Run(async () =>
            {
                service.AddJob(new JobService(() =>
                {
                    countRun++;
                    return Task.CompletedTask;
                }).ToRunEvery(new DateTime().AddMilliseconds(timeoutJob).TimeOfDay));

                await Task.Delay(timeout);
            });

            Assert.IsTrue(countRun == result);
        }
    }
}
