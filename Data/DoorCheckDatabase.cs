using Android.OS;
using DoorChecker.Models;
using SQLite;

namespace DoorChecker.Data
{
    public class DoorCheckDatabase
    {
        SQLiteAsyncConnection database;

        public async Task Init()
        {
            if (database is not null)
                return;

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await database.CreateTableAsync<Models.Location>();
            await database.CreateTableAsync<Door>();
            await database.CreateTableAsync<CheckLog>();

            InitLocations();
            InitDoors();
        }

        private async void InitLocations()
        {
            await database.DeleteAllAsync<Models.Location>();

            List<Models.Location> locations = new List<Models.Location>()
            {
                new Models.Location() { Name = "Headquarters" },
                new Models.Location() { Name = "Warehouse" },
                new Models.Location() { Name = "Remote Office" }
            };

            foreach (var location in locations)
            {
                await database.InsertAsync(location);
            }
        }

        private async void InitDoors()
        {
            await database.DeleteAllAsync<Door>();

            List<Door> doors = new List<Door>()
            {
                new Door() { Name = "Door H1", LocationID = 1 },
                new Door() { Name = "Door H2", LocationID = 1 },
                new Door() { Name = "Door W1", LocationID = 2 },
                new Door() { Name = "Door W2", LocationID = 2 },
                new Door() { Name = "Door W3", LocationID = 2 },
                new Door() { Name = "Door RO1", LocationID = 3 },
                new Door() { Name = "Door RO2", LocationID = 3 },
                new Door() { Name = "Door RO3", LocationID = 3 },
                new Door() { Name = "Door RO4", LocationID = 3 }
            };

            foreach (var door in doors)
            {
                await database.InsertAsync(door);
            }
        }

        private async void InitCheckLogs()
        {
            await database.DeleteAllAsync<CheckLog>();

            List<CheckLog> logs = new List<CheckLog>()
            {
                new CheckLog() { CheckDate = DateTime.Today.AddDays(-2), Username = "Okan", DoorID = 1, Check1 = true, Check4 = true },
                new CheckLog() { CheckDate = DateTime.Today.AddDays(-2), Username = "Okan", DoorID = 1, Check1 = true, Check4 = true },
                new CheckLog() { CheckDate = DateTime.Today.AddDays(-3), Username = "Ertan", DoorID = 2, Check2 = true, Check5 = true },
                new CheckLog() { CheckDate = DateTime.Today.AddDays(-3), Username = "Ertan", DoorID = 2, Check2 = true, Check5 = true },
                new CheckLog() { CheckDate = DateTime.Today.AddDays(-4), Username = "Okan", DoorID = 3, Check3 = true, Check6 = true },
            };

            foreach (var log in logs)
            {
                await database.InsertAsync(log);
            }
        }

        public async Task<List<Models.Location>> GetLocationsAsync()
        {
            return await database.Table<Models.Location>().ToListAsync();
        }

        public async Task<Models.Location> GetLocationAsync(int id)
        {
            return await database.Table<Models.Location>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<Door>> GetDoorsByLocationAsync(int locationID)
        {
            return await database.Table<Door>().Where(i => i.LocationID == locationID).ToListAsync();
        }

        public async Task<Door> GetDoorAsync(int id)
        {
            return await database.Table<Door>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<CheckLog>> GetCheckLogsAsync()
        {
            return await database.Table<CheckLog>().ToListAsync();
        }

        public async Task<CheckLog> GetCheckLogAsync(int id)
        {
            return await database.Table<CheckLog>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveCheckLogAsync(CheckLog item)
        {
            if (item.ID != 0)
            {
                return await database.UpdateAsync(item);
            }
            else
            {
                return await database.InsertAsync(item);
            }
        }

        public async Task<int> DeleteCheckLogAsync(CheckLog item)
        {
            return await database.DeleteAsync(item);
        }
    }
}
