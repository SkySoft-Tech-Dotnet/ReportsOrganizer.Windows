using Newtonsoft.Json;
using System;
using System.IO;
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
        public T Value { get; set; }

        public ConfigurationService(string fileName)
        {
            Value = File.Exists(fileName)
                ? LoadAsync(fileName).Result
                : Activator.CreateInstance(typeof(T)) as T;
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }

        private Task<T> LoadAsync(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                T item = JsonConvert.DeserializeObject<T>(json);
                return Task.FromResult(item);
            }
        }
    }
}
