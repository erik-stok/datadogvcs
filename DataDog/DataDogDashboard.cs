using Newtonsoft.Json;

namespace DataDog
{
    public class DataDogDashboard
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public ulong Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
        
    }
}
