using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReportsOrganizer.UI.Models
{
    public class ApplicationSettings
    {
        [JsonProperty("general")]
        public ApplicationGeneralSettings General { get; set; }

        [JsonProperty("notification")]
        public ApplicationNotificationSettings Notification { get; set; }

        [JsonProperty("personalization")]
        public ApplicationPersonalizationSettings Personalization { get; set; }
    }

    public class ApplicationGeneralSettings
    {
        [JsonProperty("language")]
        public string Language { get; set; }
    }

    public class ApplicationNotificationSettings
    {
        [JsonProperty("enable_interval")]
        public bool EnableInterval { get; set; }

        [JsonProperty("enable_at_time")]
        public bool EnableAtTime { get; set; }

        [JsonProperty("enable_ignore_time")]
        public bool EnableIgnoreTime { get; set; }

        [JsonProperty("interval")]
        public ApplicationNotification Interval { get; set; }

        [JsonProperty("at_time")]
        public IEnumerable<ApplicationNotification> AtTimes { get; set; }

        [JsonProperty("ignore_time")]
        public IEnumerable<ApplicationNotificationInterval> IgnoreTimes { get; set; }
    }

    public class ApplicationPersonalizationSettings
    {
        [JsonProperty("theme")]
        public string Theme { get; set; }
    }
}
