using System;

namespace ReportsOrganizer.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Project Project { get; set; }
    }
}
