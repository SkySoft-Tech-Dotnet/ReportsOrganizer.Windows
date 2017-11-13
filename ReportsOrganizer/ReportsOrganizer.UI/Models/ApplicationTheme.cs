using System;
using System.Collections.Generic;

namespace ReportsOrganizer.UI.Models
{
    public class ApplicationTheme
    {
        public Dictionary<string, Uri> Themes { get; set; }

        public ApplicationTheme()
        {
            Themes = new Dictionary<string, Uri>();
        }
    }
}
