using sports_appointments.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sports_appointments.Services
{
    public static class CourtService
    {
        
        static async Task Init()
        {
            await Organization.InitializeOrg();
        }
        public static async Task AddCourt(string name, string address, int capacity)
        {
            await Init();
            if(Organization.db != null)
            {
                var newCourt = new Court
                {
                    name = name,
                    address = address,
                    capacity = capacity,
                };
                await Organization.db.InsertAsync(newCourt);
            }
        }
        public static async Task RemoveCourt(int id)
        {
            await Init();
            await Organization.db.DeleteAsync<Court>(id);
        }
        public static async Task<IEnumerable<Court>> GetCourts()
        {
            await Init();
            var courts = await Organization.db.Table<Court>().ToListAsync();
            return courts;
        }
        public static async Task<Court> GetCourt(int courtId)
        {
            await Init();
            var courts = await Organization.db.Table<Court>().ToListAsync();
            return courts.Find(x => x.courtId == courtId);
        }
        public static async Task UpdateCourt(Court court)
        {
            await Init();
            await Organization.db.UpdateAsync(court);
        }
    }
}
