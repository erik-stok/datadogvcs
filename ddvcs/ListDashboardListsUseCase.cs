using DataDog;

namespace ddvcs
{
    public class ListDashboardListsUseCase
    {
        private readonly IOutputWriter _outputWriter;
        private readonly IDataDogGateway _dataDogGateway;

        public ListDashboardListsUseCase(IOutputWriter outputWriter, IDataDogGateway dataDogGateway)
        {
            _dataDogGateway = dataDogGateway;
            _outputWriter = outputWriter;
        }

        public bool Execute()
        {
            var dashboardsLists = _dataDogGateway.GetDashboardLists();

            
            if (dashboardsLists.Count == 0)
            {
                _outputWriter.Write("No dashboard lists available");
                return false;
            }

            foreach (DataDogDashboardListsItem dashboardList in dashboardsLists)
            {
                _outputWriter.Write(dashboardList.Name);
            }

            return true;
        }
    }
}
