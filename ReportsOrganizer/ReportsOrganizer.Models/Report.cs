using System;

namespace ReportsOrganizer.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime Created { get; set; }

        public Project Project { get; set; }

        public override string ToString()
        {
            return
                $"{Description}\t" +
                $"{Created.ToShortDateString()}\t" +
                $"{Duration}\t\t" +
                $"{Project?.ShortName}\t";
        }
    }
}
