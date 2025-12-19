using DoorChecker.Data;
using static Android.Provider.CallLog;

namespace DoorChecker.Models
{
    public class CheckItemViewModel
    {
        private DoorCheckDatabase database;

        public int ID { get; set; }
        public DateTime CheckDate { get; set; }
        public string Username { get; set; }
        public string ReaderType { get; set; }
        public int LocationID { get; set; }
        public int DoorID { get; set; }
        public bool Check1 { get; set; }
        public bool Check2 { get; set; }
        public bool Check3 { get; set; }
        public bool Check4 { get; set; }
        public bool Check5 { get; set; }
        public bool Check6 { get; set; }
        public bool Check7 { get; set; }
        public bool Check8 { get; set; }
        public bool Check9 { get; set; }
        public bool Check10 { get; set; }
        public bool Check11 { get; set; }
        public bool Check12 { get; set; }
        public byte[] ReaderIn { get; set; }
        public byte[] ReaderOut { get; set; }
        public byte[] Lock1 { get; set; }
        public byte[] Lock2 { get; set; }
        public byte[] BGU { get; set; }
        public byte[] RTE { get; set; }

        public List<Location> Locations { get; set; }
        public List<Door> Doors { get; set; }


        public CheckItemViewModel(DoorCheckDatabase database, int checkLogID)
        {
            this.database = database;
            Task.Run(() => LoadCheckItem(checkLogID)).Wait(); 
        }

        private async Task LoadCheckItem(int checkLogID)
        {
            if (checkLogID == 0)
            {
                await LoadCombos();
                this.CheckDate = DateTime.Today;
                this.Username = Environment.UserName;
                return;
            }

            var item = await database.GetCheckLogAsync(checkLogID);
            var door = await database.GetDoorAsync(item.DoorID);
            var location = await database.GetLocationAsync(door.LocationID);

            this.ID = item.ID;
            this.CheckDate = item.CheckDate;
            this.Username = item.Username;
            this.LocationID = location.ID;
            this.DoorID = door.ID;
            this.ReaderType = item.ReaderType;
            this.Check1 = item.Check1;
            this.Check2 = item.Check2;
            this.Check3 = item.Check3;
            this.Check4 = item.Check4;
            this.Check5 = item.Check5;
            this.Check6 = item.Check6;
            this.Check7 = item.Check7;
            this.Check8 = item.Check8;
            this.Check9 = item.Check9;
            this.Check10 = item.Check10;
            this.Check11 = item.Check11;
            this.Check12 = item.Check12;
            this.ReaderIn = item.ReaderIn;
            this.ReaderOut = item.ReaderOut;
            this.Lock1 = item.Lock1;
            this.Lock2 = item.Lock2;
            this.BGU = item.BGU;
            this.RTE = item.RTE;

            await LoadCombos();
        }

        private async Task LoadCombos()
        {
            var locations = await database.GetLocationsAsync();
            var doors = await database.GetDoorsByLocationAsync(LocationID);
            Locations = locations;
            Doors = doors;

        }

        public void AddOrUpdateCheckLog(CheckLog item)
        {
            Task.Run(() => database.SaveCheckLogAsync(item)).Wait();
        }

        public async Task RefreshDoors()
        {
            var doors = await database.GetDoorsByLocationAsync(LocationID);
            Doors = doors;
        }

        public void DeleteCheckLog(int ID)
        {
            var item = Task.Run(() => database.GetCheckLogAsync(ID)).Result;
            Task.Run(() => database.DeleteCheckLogAsync(item)).Wait();
        }
    }
}
