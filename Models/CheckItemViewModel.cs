using DoorChecker.Data;

namespace DoorChecker.Models
{
    public class CheckItemViewModel
    {
        private DoorCheckDatabase database;

        public int ID { get; set; }
        public int Location { get; set; }
        public int Door { get; set; }
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

        public List<Location> Locations { get; set; }
        public List<Door> Doors { get; set; }


        public CheckItemViewModel(DoorCheckDatabase database, int checkLogID)
        {
            this.database = database;
            Task.Run(async () => await LoadCheckItem(checkLogID)); 
        }

        private async Task LoadCheckItem(int checkLogID)
        {
            if (checkLogID == 0)
            {
                await LoadCombos();
                return;
            }

            var item = await database.GetCheckLogAsync(checkLogID);
            var door = await database.GetDoorAsync(item.DoorID);
            var location = await database.GetLocationAsync(door.LocationID);

            this.ID = item.ID;
            this.Location = location.ID;
            this.Door = door.ID;
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

            await LoadCombos();
        }

        private async Task LoadCombos()
        {
            var locations = await database.GetLocationsAsync();
            var doors = await database.GetDoorsByLocationAsync(Location);
            Locations = locations;
            Doors = doors;

        }
    }
}
