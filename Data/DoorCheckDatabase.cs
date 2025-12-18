using DoorChecker.Models;
using SQLite;

namespace DoorChecker.Data
{
    public class DoorCheckDatabase
    {
        SQLiteAsyncConnection database;

        async Task Init()
        {
            if (database is not null)
                return;

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await database.CreateTableAsync<Models.Location>();
            result = await database.CreateTableAsync<Door>();
            result = await database.CreateTableAsync<CheckLog>();

            InitLocations();
            InitDoors();
            InitCheckLogs();
        }

        private async void InitLocations()
        {
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
            List<Door> doors = new List<Door>()
            {
                new Door() { Name = "Door H1", LocationID = 1 },
                new Door() { Name = "Door H2", LocationID = 1 },
                new Door() { Name = "Door W1", LocationID = 2 },
                new Door() { Name = "Door W2", LocationID = 2 },
                new Door() { Name = "Door RO1", LocationID = 3 },
                new Door() { Name = "Door RO2", LocationID = 3 }
            };

            foreach (var door in doors)
            {
                await database.InsertAsync(door);
            }
        }

        private async void InitCheckLogs()
        {
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
            await Init();
            return await database.Table<Models.Location>().ToListAsync();
        }

        public async Task<Models.Location> GetLocationAsync(int id)
        {
            await Init();
            return await database.Table<Models.Location>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<Door>> GetDoorsByLocationAsync(int locationID)
        {
            await Init();
            return await database.Table<Door>().Where(i => i.LocationID == locationID).ToListAsync();
        }

        public async Task<Door> GetDoorAsync(int id)
        {
            await Init();
            return await database.Table<Door>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<CheckLog>> GetCheckLogsAsync()
        {
            await Init();
            return await database.Table<CheckLog>().ToListAsync();
        }

        public async Task<CheckLog> GetCheckLogAsync(int id)
        {
            await Init();
            return await database.Table<CheckLog>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveCheckLogAsync(CheckLog item)
        {
            await Init();
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
            await Init();
            return await database.DeleteAsync(item);
        }
    }
}
