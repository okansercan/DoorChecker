using DoorChecker.Data;

namespace DoorChecker.Models
{
    public class CheckListViewModel
    {
        private DoorCheckDatabase database;
        public List<CheckItem> CheckItems { get; set; }
        public CheckItem SelectedItem { get; set; }

        public CheckListViewModel(DoorCheckDatabase database)
        {
            CheckItems = new List<CheckItem>();
            this.database = database;
            LoadCheckItems();
        }

        private async void LoadCheckItems()
        {
            var items = await database.GetCheckLogsAsync();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                CheckItems.Clear();
                foreach (var item in items)
                {
                    var door = database.GetDoorAsync(item.DoorID).Result;
                    var location = database.GetLocationAsync(door.LocationID).Result;

                    var checkItem = new CheckItem
                    {
                        ID = item.ID,
                        CheckDate = item.CheckDate.ToString("yyyy-MM-dd"),
                        Username = item.Username,
                        LocationName = location.Name,
                        DoorName = door.Name
                    };

                    CheckItems.Add(checkItem);
                }
            });
        }
    }

    public class CheckItem
    {
        public int ID { get; set; }
        public string CheckDate { get; set; }
        public string Username { get; set; }
        public string LocationName { get; set; }
        public string DoorName { get; set; }

        public CheckItem()
        {
            CheckDate = string.Empty;
            Username = string.Empty;
            LocationName = string.Empty;
            DoorName = string.Empty;
        }
    }
}
