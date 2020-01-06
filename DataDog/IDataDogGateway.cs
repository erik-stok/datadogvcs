using System;
using System.Collections.Generic;

namespace DataDog
{
    public interface IDataDogGateway
    {
        bool ApiKeyIsValid();

        IReadOnlyCollection<DataDogDashboardListsItem> GetDashboardLists();

        IReadOnlyCollection<DataDogDashboard> GetDashboardListItems(ulong dashboardListId);

        DataDogDashboardContent GetDashboardContent(ulong dashboardId, string dashboardType);
    }
}
