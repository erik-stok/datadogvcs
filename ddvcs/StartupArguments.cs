using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace ddvcs
{
    public class StartupArguments
    {
        public string BaseUri { get; set; }
        public string ApiKey { get; set; }
        public string AppKey { get; set; }
        public string Folder { get; set; }
        public string DashboardList { get; set; }
        public bool ShouldValidateKey { get; set; }
        public bool ShouldListDashboardLists { get; set; }
        public bool ShouldListDashboards { get; set; }
        public bool ShouldPullDashboards { get; set; }

        public StartupArguments()
        {
            BaseUri = "";
            ApiKey = "";
            AppKey = "";
            Folder = "";
            DashboardList = "";
            ShouldValidateKey = false;
            ShouldListDashboardLists = false;
            ShouldListDashboards = false;
            ShouldPullDashboards = false;
        }

        string ArgumentIdentifier(string argument)
        {
            if (argument.IndexOf(":", 0, StringComparison.Ordinal) >= 0)
            {
                return argument.Substring(0, argument.IndexOf(":", 0, StringComparison.Ordinal));
            }
            return argument;
        }

        string ArgumentValue(string argument)
        {
            if (argument.IndexOf(":", 0, StringComparison.Ordinal) >= 0)
            {
                return argument.Substring(argument.IndexOf(":", 0, StringComparison.CurrentCulture) + 1);
            }
            return "";
        }

        void ReadArgument(string argument)
        {
            var argumentIdentifier = ArgumentIdentifier(argument).ToLower();
            var argumentValue = ArgumentValue(argument);

            switch (argumentIdentifier)
            {
                case "apikey":
                case "a":
                    ApiKey = argumentValue;
                    break;
                case "applicationkey":
                case "k":
                    AppKey = argumentValue;
                    break;
                case "uri":
                case "u":
                    BaseUri = argumentValue;
                    break;
                case "folder":
                case "f":
                    Folder = argumentValue;
                    break;
                case "dashboardlist":
                case "d":
                    DashboardList = argumentValue;
                    break;
                case "validatekey":
                case "v":
                    ShouldValidateKey = true;
                    break;
                case "list":
                case "l":
                    ShouldListDashboardLists = true;
                    break;
                case "content":
                case "c":
                    ShouldListDashboards = true;
                    break;
                case "pull":
                case "p":
                    ShouldPullDashboards = true;
                    break;
                default:
                    throw new Exception("Invalid argument: " + argumentIdentifier);
                
            }
        }
        
        public string ReadArguments(string[] arguments)
        {
            foreach (var argument in arguments)
            {
                ReadArgument(argument);
            }

            if (ShouldPullDashboards && (Folder == ""))
            {
                Folder = ".";
            }

            if (ShouldValidateKey && (ShouldListDashboardLists || ShouldListDashboards || ShouldPullDashboards))
            {
                return "Cannot validate key with any other commands";
            }

            if (ShouldValidateKey && (ApiKey == ""))
            {
                return "Cannot validate key without any api key";
            }

            if (!ShouldValidateKey && ((ApiKey == "") || (AppKey == "")))
            {
                return "Cannot perform commands without Api key and Application key";
            }

            if (ShouldListDashboardLists && (ShouldValidateKey || ShouldListDashboards || ShouldPullDashboards))
            {
                return "Cannot list dashboard lists with any other commands";
            }

            if (ShouldListDashboards && (ShouldValidateKey || ShouldListDashboardLists || ShouldPullDashboards))
            {
                return "Cannot list dashboard list content with any other commands";
            }

            if (ShouldPullDashboards && (ShouldValidateKey || ShouldListDashboardLists || ShouldListDashboards))
            {
                return "Cannot pull dashboard list with any other commands";
            }

            if (ShouldListDashboards && (DashboardList == ""))
            {
                return "Cannot list dashboard list content if no dashboard list is specified";
            }

            if (ShouldPullDashboards && (DashboardList == ""))
            {
                return "Cannot pull dashboard list if no dashboard list is specified";
            }

            if (ShouldValidateKey || ShouldListDashboardLists || ShouldListDashboards || ShouldPullDashboards)
            {
                return "";
            }

            return "Please supply a command";
        }
    }
}
