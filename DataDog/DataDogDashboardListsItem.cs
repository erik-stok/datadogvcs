using Newtonsoft.Json;

namespace DataDog
{
    public class DataDogDashboardListsItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public ulong Id { get; set; }
    }
}
