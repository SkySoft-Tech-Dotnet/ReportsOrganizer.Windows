using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Test.Extensions
{
    public static class AsyncTestExtensions
    {
        public static void RethrowForCompletedTasks(this Task task)
        {
            task.GetAwaiter().GetResult();
        }
    }
}
