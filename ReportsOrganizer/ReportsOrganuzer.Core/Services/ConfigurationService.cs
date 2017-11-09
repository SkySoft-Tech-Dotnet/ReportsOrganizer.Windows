using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReportsOrganizer.Core.Services
{
    public interface IConfigurationService<T>
        where T : class
    {
        T Value { get; set; }

        Task UpdateAsync();
    }

    internal class ConfigurationService<T> : IConfigurationService<T>
        where T : class
    {
        private static readonly object _writeLock = new object();
        private readonly string _fileName;

        public T Value { get; set; }

        public ConfigurationService(string fileName)
        {
            _fileName = fileName;
            Value = File.Exists(_fileName)
                ? LoadAsync().Result
                : throw new FileNotFoundException("File does not exist.", _fileName);
            //: Activator.CreateInstance(typeof(T)) as T;
        }

        public async Task UpdateAsync()
        {
            lock (_writeLock)
            {
                if (Value == null)
                    return;

                var json = JsonConvert.SerializeObject(Value);

                using (var fileStream = new FileStream(_fileName,
                    FileMode.Create, FileAccess.Write, FileShare.None,
                    bufferSize: json.Length, useAsync: true))
                {
                    fileStream.WriteAsync(Encoding.ASCII.GetBytes(json), 0, json.Length).Wait();
                }
            }
        }

        private Task<T> LoadAsync()
        {
            using (var r = new StreamReader(_fileName))
            {
                var json = r.ReadToEnd();
                var item = JsonConvert.DeserializeObject<T>(json);
                return Task.FromResult(item);
            }
        }
    }
}
