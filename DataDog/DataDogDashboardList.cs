using Newtonsoft.Json;

namespace DataDog
{
    public class DataDogDashboardList
    {
        [JsonProperty("dashboards")]
        public DataDogDashboard[] Dashboards { get; set; }
    }
}
