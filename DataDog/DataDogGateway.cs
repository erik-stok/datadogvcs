using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RestSharp;

namespace DataDog
{
    public class DataDogGateway : IDataDogGateway
    {
        private readonly string _apiKey;
        private readonly string _appKey;
        private readonly RestClient _restClient;

        private const string BaseUri = "https://api.datadoghq.com/api/v1/";

        private const string ApiKeyParameter = "api_key";
        private const string AppKeyParameter = "application_key";

        private const string ValidateResource = "validate";
        private const string DashboardListsResource = "dashboard/lists/manual";
        private const string DashboardListResource = "dashboard/lists/manual/{0}/dashboards";
        private const string ScreenDashboardContentResource = "screen/{0}";
        private const string TimeDashboardContentResource = "dash/{0}";

        public DataDogGateway(string apiKey, string appKey)
        {
            _apiKey = apiKey;
            _appKey = appKey;

            _restClient = CreateRestClient();
        }

        private RestClient CreateRestClient()
        {
            var restClient = new RestClient(BaseUri);

            var deserializer = NewtonsoftJsonSerializer.Default;

            restClient.AddHandler("application/json", deserializer);
            restClient.AddHandler("text/json", deserializer);
            restClient.AddHandler("text/x-json", deserializer);
            restClient.AddHandler("text/javascript", deserializer);
            restClient.AddHandler("*+json", deserializer);

            return restClient;
        }
        private RestRequest NewRequest(string resource, bool useAppKey = true)
        {
            var request = new RestRequest(resource, Method.GET, DataFormat.Json);
            request.AddParameter(ApiKeyParameter, _apiKey);

            if (useAppKey)
            {
                request.AddParameter(AppKeyParameter, _appKey);
            }
            

            return request;
        }

        public bool ApiKeyIsValid()
        {
            var request = NewRequest(ValidateResource, false);

            var response = _restClient.Execute(request);

            return response.IsSuccessful;
        }

        public IReadOnlyCollection<DataDogDashboardListsItem> GetDashboardLists()
        {
            var request = NewRequest(DashboardListsResource);

            var response = _restClient.Execute<DataDogDashboardLists>(request);

            if (response.Data != null)
            {
                return response.Data.DashboardListsItems;
            }

            return new List<DataDogDashboardListsItem>();
        }

        public IReadOnlyCollection<DataDogDashboard> GetDashboardListItems(ulong dashboardListId)
        {
            var request = NewRequest(String.Format(DashboardListResource, dashboardListId));

            var response = _restClient.Execute<DataDogDashboardList>(request);

            if (response.Data != null)
            {
                return response.Data.Dashboards;
            }

            return new List<DataDogDashboard>();
        }

        public DataDogDashboardContent GetDasbboardContent(ulong dashboardId, string dashboardType)
        {
            RestRequest request;

            if (dashboardType.Contains("screenboard", StringComparison.CurrentCulture))
            {
                request = NewRequest(String.Format(ScreenDashboardContentResource, dashboardId));
            }
            else
            {
                request = NewRequest(String.Format(TimeDashboardContentResource, dashboardId));

            }

            var response = _restClient.Execute(request);

            if (response.IsSuccessful)
            {
                return new DataDogDashboardContent { Id = dashboardId, Json = response.Content };
            }

            return new DataDogDashboardContent { Id = dashboardId, Json = "" };
        }

    }
}
