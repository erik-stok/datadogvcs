﻿using System;
using System.Linq;
using SimpleInjector;

namespace ddvcs
{
    class Program
    {
        static int Main(string[] args)
        {
            var startupArguments = new StartupArguments();
            var startupError = startupArguments.ReadArguments(args);

            var container = new Container();

            if (startupError != "")
            {
                container.GetInstance<OutputWriter>().Write(
                    "DataDog VCS 0.1\n" +
                    "\n" +
                    startupError +
                    "\n\n" +
                    "Usage:\n" +
                    "\n" +
                    "ddvcs.dll ApiKey:<DataDog Api key> [ValidateKey] [ApplicationKey:<DataDog application key>] " +
                    "[List] [Dashboard:<Dashboard to use>] [Content] [Pull] [Folder:<Folder to use for a pull>]\n" +
                    "\n" +
                    "Arguments:\n" +
                    "\n" +
                    "ApiKey:         Sets the Api key to use when connecting to DataDog.\n" +
                    "ValidateKey:    Invokes the DataDag Api to validate the given Api key.\n" +
                    "ApplicationKey: Sets the Application key to use when connecting to DataDog.\n" +
                    "List:           Shows a list of dashboard lists that can be used.\n" +
                    "DashboardList:  Sets the dashboard list a 'Content' or 'Pull' operation will be performed on.\n" +
                    "Content:        Shows the dashboards in the dashboard list specified.\n" +
                    "Pull:           Pulls the dashboard list specified to the given folder.\n" +
                    "Folder:         Folder to pull the dashboard of the list to as files.\n");

                return -1;
            }

            var bootstrapper = new Bootstrapper();
            bootstrapper.Register(container, startupArguments.ApiKey, startupArguments.AppKey, startupArguments.BaseUri, startupArguments.Folder);

            if (startupArguments.ShouldValidateKey)
            {
                container.GetInstance<ValidateKeyUseCase>().Execute();
            }

            if (startupArguments.ShouldListDashboardLists)
            {
                container.GetInstance<ListDashboardListsUseCase>().Execute();
            }

            if (startupArguments.ShouldListDashboards)
            {
                container.GetInstance<ListDashboardsUseCase>().Execute(startupArguments.DashboardList);
            }

            if (startupArguments.ShouldPullDashboards)
            {
                container.GetInstance<PullDashboardsUseCase>().Execute(startupArguments.DashboardList);
            }

            return 0;
        }
    }
}
