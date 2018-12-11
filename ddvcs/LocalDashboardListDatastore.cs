using System;
using System.Collections.Generic;
using System.IO;
using DataDog;

namespace ddvcs
{
    public class LocalDashboardListDatastore : ILocalDashboardListDatastore
    {
        private const string FilePrefix = "dashboard_";
        private const string FileExtension = ".json";
        
        private readonly string _path;

        public LocalDashboardListDatastore(string path)
        {

            _path = path;
        }

        public void Clear()
        {
            var files = Directory.EnumerateFiles(_path);

            foreach (string file in files)
            {
                if (file.StartsWith(Path.Combine(_path, FilePrefix)))
                {
                    File.Delete(file);
                }
            }
        }

        public void Write(ulong dashboardId, IReadOnlyCollection<DataDogDashboardContent> dashboardContents)
        {
            foreach (DataDogDashboardContent dashboardContent in dashboardContents)
            {
                var filename = Path.Combine(_path, string.Concat(FilePrefix, dashboardContent.Id.ToString(), FileExtension));
                File.WriteAllText(filename, JsonHelper.FormatJson(dashboardContent.Json));
            }
        }
    }
}
