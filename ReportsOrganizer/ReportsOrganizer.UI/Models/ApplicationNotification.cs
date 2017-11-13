using Newtonsoft.Json;

namespace ReportsOrganizer.UI.Models
{
    public class ApplicationNotification
    {
        [JsonProperty("hour")]
        public int Hour { get; set; }

        [JsonProperty("minute")]
        public int Minute { get; set; }
    }

    public class ApplicationNotificationInterval
    {
        [JsonProperty("begin")]
        public ApplicationNotification BeginInterval { get; set; }

        [JsonProperty("end")]
        public ApplicationNotification EndInterval { get; set; }
    }
}
