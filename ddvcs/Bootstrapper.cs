using System;
using DataDog;
using SimpleInjector;

namespace ddvcs
{
    public class Bootstrapper
    {
        public void Register(Container container, String apiKey, String appKey, String folder)
        {
            container.Register<IOutputWriter, OutputWriter>();
            container.Register<IDataDogGateway>(() => new DataDogGateway(apiKey, appKey));
            container.Register<ILocalDashboardListDatastore>(() => new LocalDashboardListDatastore(folder));
            container.Register<ValidateKeyUseCase>();
            container.Register<ListDashboardListsUseCase>();
            container.Register<ListDashboardsUseCase>();
            container.Register<PullDashboardsUseCase>();
        }
    }
}
