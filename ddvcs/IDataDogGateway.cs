using System;
using System.Collections.Generic;
using System.Text;

namespace ddvcs
{
    public interface IDataDogGateway
    {
        bool ApiKeyIsValid();

        IReadOnlyCollection<DataDogDashboardList> GetDashboardLists();

        IReadOnlyCollection<DataDogDashboard> GetDashboardList(Int64 dashboardListId);
    }
}
