using System;
using System.Collections.Generic;
using System.Text;

namespace ddvcs
{
    public class DataDogDashboardList
    {
        public string Name { get; }
        public long Id { get; }

        public DataDogDashboardList(string name, long id)
        {
            Name = name;
            Id = id;
        }
    }
}
