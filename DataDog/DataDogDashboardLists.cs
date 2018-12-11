using Newtonsoft.Json;

namespace DataDog
{
    public class DataDogDashboardLists
    {
        [JsonProperty("dashboard_lists")]
        public DataDogDashboardListsItem[] DashboardListsItems { get; set; }
    }
}
