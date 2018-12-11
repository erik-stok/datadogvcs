using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ddvcs
{
    public class DataDogGateway : IDataDogGateway
    {
        public bool ApiKeyIsValid()
        {
            return true;
        }

        public IReadOnlyCollection<DataDogDashboardList> GetDashboardLists()
        {
            var dashBoardLists = new List<DataDogDashboardList>();

            dashBoardLists.Add(new DataDogDashboardList("List 1", 1));
            dashBoardLists.Add(new DataDogDashboardList("List 2", 2));

            return new ReadOnlyCollection<DataDogDashboardList>(dashBoardLists);
        }

        public IReadOnlyCollection<DataDogDashboard> GetDashboardList(Int64 dashboardListId)
        {
            var dashBoardList = new List<DataDogDashboard>();

            dashBoardList.Add(new DataDogDashboard("Dashboard 1", 100));
            dashBoardList.Add(new DataDogDashboard("Dashboard 2", 200));

            return new ReadOnlyCollection<DataDogDashboard>(dashBoardList);
        }

    }
}
