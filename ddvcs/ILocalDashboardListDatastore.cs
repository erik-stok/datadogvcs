using System;
using System.Collections.Generic;
using DataDog;

namespace ddvcs
{
    public interface ILocalDashboardListDatastore
    {
        void Clear();

        void Write(ulong dashboardId, IReadOnlyCollection<DataDogDashboardContent> dashboardContents);
    }
}
