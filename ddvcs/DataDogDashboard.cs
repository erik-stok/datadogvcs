using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ddvcs
{
    public class DataDogDashboard
    {
        private List<DataDogDashboardItem> _items;

        public string Name { get; }
        public long Id { get; }

        public DataDogDashboard(string name, long id)
        {
            Name = name;
            Id = id;

            _items = new List<DataDogDashboardItem>();
        }

        public void AddItem(DataDogDashboardItem item)
        {
            _items.Add(item);
        }

        public IReadOnlyCollection<DataDogDashboardItem> GetItems()
        {
            return new ReadOnlyCollection<DataDogDashboardItem>(_items);
        }
    }
}
