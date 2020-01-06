using System.Collections.Generic;
using System.Linq;
using DataDog;

namespace ddvcs
{
    public class PullDashboardsUseCase
    {
        private readonly IOutputWriter _outputWriter;
        private readonly IDataDogGateway _dataDogGateway;
        private readonly ILocalDashboardListDatastore _localDashboardListDatastore;

        public PullDashboardsUseCase(IOutputWriter outputWriter, IDataDogGateway dataDogGateway, ILocalDashboardListDatastore localDashboardListDatastore)
        {
            _outputWriter = outputWriter;
            _dataDogGateway = dataDogGateway;
            _localDashboardListDatastore = localDashboardListDatastore;
        }

        public void Execute(string dashboardList)
        {
            var dashboardLists = _dataDogGateway.GetDashboardLists();

            var selectedDashboardList = dashboardLists.First(d => d.Name == dashboardList);

            if (selectedDashboardList != null)
            {
                var dashboardListItems = _dataDogGateway.GetDashboardListItems(selectedDashboardList.Id);

                var dashboardContents = new List<DataDogDashboardContent>();

                foreach (var dashboardListItem in dashboardListItems)
                {
                    dashboardContents.Add(_dataDogGateway.GetDashboardContent(dashboardListItem.Id, dashboardListItem.Type));
                }

                _localDashboardListDatastore.Clear();
                _localDashboardListDatastore.Write(selectedDashboardList.Id, dashboardContents);
            }
            else
            {
                _outputWriter.Write("Dashboard list not found");
            }
        }
    }
}
