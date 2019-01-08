# DataDog VCS
When creating dashboards in [DataDog](https://www.datadoghq.com), there is no facility to keep track of changes. That makes it hard to see a revision history of a dashboard that was configured. Especially when working on dashboards with multiple developers, this can introduce problems when poor changes have been made.

DataDog VCS is a tool that uses the DataDog API to allow developers to synchronize a DataDog dashboard list with a local copy of the Json representation of the dashboards in that list, so version control can be done.

## Requirements

DataDog VCS is a .Net core 2.1 application, so to get it up and running make sure you have at least installed the .Net core 2.1 runtime.

Either build the application yourself or download the latest build to be able to start the application.

## How to use

In order to put your dashboards into version control, put the dashboard you would like to have versioned into a single [dashboard list](https://www.datadoghq.com/blog/dashboard-lists). That dashboard list is used by DataDog VCS to determine what dashboards to pull to your local machine for version control.

Then synchronize the local copy of the dashboards with your version control system as you see fit.

DataDog VCS is a command line tool. With the arguments of the command line tool, you can instruct it to perform several operations.

Use **dotnet ddvcs.dll ApiKey:\<DataDog Api key\> \[ValidateKey\] \[ApplicationKey:\<DataDog Application key\>\] \[List\] \[Dashboard:\<Dashboard to use>\] \[Content\] \[Pull\] \[Folder:\<Folder to use\>]**

| Argument       | Meaning                                                      |
| -------------- | ------------------------------------------------------------ |
| ApiKey         | The Api key to connect to DataDog via the API.               |
| ValidateKey    | Instruction to validate of the specified DataDag Api key can connect via the Api. |
| ApplicationKey | The application key to use in combination with the Api key in order to perform operations. Each operation other than ValidateKey requires both the ApiKey and the ApplicationKey argument. |
| List           | List the dashboard lists that can be operated on.            |
| DashboardList  | Selects a dashboard list by its name for a content scan or a pull. |
| Content        | Scan the dashboards in the given dashboard list.             |
| Pull           | Pulls the given dashboard list into the given folder.        |
| Folder         | When performing a pull of dashboard definitions, this folder will receive the files. |


