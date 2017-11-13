using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private Stack<CancellationTokenSource> _cancellationTokensSource;

        private object _synchronizing;
        private object _stackSynchronizing;

        public T Value { get; set; }

        public ApplicationOptions(string path)
        {
            _path = path;
            _cancellationTokensSource = new Stack<CancellationTokenSource>();

            _synchronizing = new object();
            _stackSynchronizing = new object();
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
            var cancellationTokenSource = new CancellationTokenSource();

            lock (_stackSynchronizing)
            {
                while (_cancellationTokensSource.Count != 0)
                {
                    _cancellationTokensSource.Pop().Cancel();
                }

                _cancellationTokensSource.Push(cancellationTokenSource);
                cancellationToken.Register(cancellationTokenSource.Cancel);
            }

            var content = JsonConvert.SerializeObject(Value);
            var contentEncode = Encoding.ASCII.GetBytes(content);

            lock (_synchronizing)
            {
                using (FileStream stream = File.Open(_path, FileMode.Create))
                {
                    stream.Seek(0, SeekOrigin.End);
                    try
                    {
                        stream.WriteAsync(
                            contentEncode, 0, contentEncode.Length, cancellationTokenSource.Token).Wait();
                    }
                    catch (AggregateException) { }
                }
            }

            cancellationTokenSource.Cancel();
            return Task.CompletedTask;
        }
    }
}
