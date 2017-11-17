using System.Collections.Generic;

namespace ReportsOrganizer.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public IEnumerable<Report> Reports { get; set; }
    }
}
