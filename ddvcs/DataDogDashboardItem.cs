using System;
using System.Collections.Generic;
using System.Text;

namespace ddvcs
{
    public class DataDogDashboardItem
    {
        public long Id { get; }
        public string Json { get; }

        public DataDogDashboardItem(long id, string json)
        {
            Id = id;
            Json = json;
        }
    }
}
