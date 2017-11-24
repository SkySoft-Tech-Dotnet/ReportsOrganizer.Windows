using System.Collections.Generic;

namespace ReportsOrganizer.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;

        public IEnumerable<Report> Reports { get; set; }

        public Project()
        {
            Reports = new List<Report>();
        }
    }
}
