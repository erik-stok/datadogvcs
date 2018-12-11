using System;
using System.Linq;
using DataDog;

namespace ddvcs
{
    public class ListDashboardsUseCase
    {

        private readonly IOutputWriter _outputWriter;
        private readonly IDataDogGateway _dataDogGateway;

        public ListDashboardsUseCase(IOutputWriter outputWriter, IDataDogGateway dataDogGateway)
        {
            _dataDogGateway = dataDogGateway;
            _outputWriter = outputWriter;
        }

        public bool Execute(string dashboardList)
        {
            var dashboardLists = _dataDogGateway.GetDashboardLists();

            var selectedDashboardList = dashboardLists.First(d => d.Name == dashboardList);

            if (selectedDashboardList == null)
            {
                return false;
            }

            var dashboardListItems = _dataDogGateway.GetDashboardListItems(selectedDashboardList.Id);

            if (dashboardListItems.Count == 0)
            {
                _outputWriter.Write("No dashboards available in dashboard list");
                return false;
            }

            foreach (var dataDogDashboard in dashboardListItems)
            {
                _outputWriter.Write(dataDogDashboard.Title);
            }

            return true;
        }
    }
}
;