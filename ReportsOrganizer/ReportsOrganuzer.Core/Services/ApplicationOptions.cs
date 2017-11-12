using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Services
{
    public interface IApplicationOptions<T>
        where T : class
    {
        T Value { get; }

        Task UpdateAsync(CancellationToken cancellationToken);
    }

    internal class ApplicationOptions<T> : IApplicationOptions<T>
        where T : class
    {
        private string _path;
        private CancellationTokenSource _lastCancellationTokenSource;
        private object _synchronizing;

        public T Value { get; set; }

        public ApplicationOptions(string path)
        {
            _path = path;
            _lastCancellationTokenSource = new CancellationTokenSource();
            _synchronizing = new object();
        }

        public async Task LoadAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            using (StreamReader reader = File.OpenText(_path))
            {
                var content = await reader.ReadToEndAsync();
                Value = JsonConvert.DeserializeObject<T>(content);
            }
        }

        public Task UpdateAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!_lastCancellationTokenSource.IsCancellationRequested)
            {
                _lastCancellationTokenSource.Cancel();
            }

            var content = JsonConvert.SerializeObject(Value);
            var contentEncode = Encoding.ASCII.GetBytes(content);

            lock (_synchronizing)
            {
                _lastCancellationTokenSource = new CancellationTokenSource();
                cancellationToken.Register(_lastCancellationTokenSource.Cancel);

                using (FileStream stream = File.Open(_path, FileMode.Create))
                {
                    stream.Seek(0, SeekOrigin.End);
                    stream.WriteAsync(
                        contentEncode, 0, contentEncode.Length, _lastCancellationTokenSource.Token).Wait();
                }
            }

            if (!_lastCancellationTokenSource.IsCancellationRequested)
            {
                _lastCancellationTokenSource.Cancel();
            }

            return Task.CompletedTask;
        }
    }
}
