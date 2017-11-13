using System.Collections.Generic;
using System.Threading.Tasks;
using ReportsOrganizer.Models;

namespace ReportsOrganizer.DAL.Base
{    

public interface IRepository<T> where T : IEntity
    {

        IEnumerable<T> List { get; }
        Task Add(string report);
        Task<Report> GetLastReport();
        //void Delete(T entity);
        //void Update(T entity);
        //T FindById(int Id);
    }
}
