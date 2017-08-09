using ReportsOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsOrganizer.DAL
{
    public partial class ReportDTO : IEntity
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public ReportDTO(int id, DateTime startDate, DateTime endDate, string description)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }

        private ReportDTO()
        {

        }
        

    }
}
