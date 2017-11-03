using Newtonsoft.Json;
using System;
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
        private string _fileName;

        public T Value { get; set; }

        public ConfigurationService(string fileName)
        {
            _fileName = fileName;
            Value = File.Exists(_fileName)
                ? LoadAsync().Result
                : Activator.CreateInstance(typeof(T)) as T;
        }

        public async Task UpdateAsync()
        {
            if (Value == null)
                return;

            var json = JsonConvert.SerializeObject(Value);

            using (FileStream fileStream = new FileStream(_fileName,
                FileMode.Create, FileAccess.Write, FileShare.None,
                bufferSize: json.Length, useAsync: true))
            {
                await fileStream.WriteAsync(Encoding.ASCII.GetBytes(json), 0, json.Length);
            }
        }

        private Task<T> LoadAsync()
        {
            using (StreamReader r = new StreamReader(_fileName))
            {
                string json = r.ReadToEnd();
                T item = JsonConvert.DeserializeObject<T>(json);
                return Task.FromResult(item);
            }
        }
    }
}
