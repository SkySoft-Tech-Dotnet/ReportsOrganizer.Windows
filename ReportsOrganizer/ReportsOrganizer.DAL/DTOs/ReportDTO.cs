using System;
using ReportsOrganizer.DAL.Base;

namespace ReportsOrganizer.DAL.DTOs
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
