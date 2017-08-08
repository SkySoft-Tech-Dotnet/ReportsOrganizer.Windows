using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOrganizer.DAL
{
    public partial class ReportDTO : IEntity
    {
        public int Id { get; private set; }
        
        public DateTime Date { get; private set; }

        public string Description { get; private set; } 

        public ReportDTO(int id, DateTime date, string description)
        {
            Id = id;
            Date = date;
            Description = description;
        }

        public ReportDTO()
        {

        }

    }
}
