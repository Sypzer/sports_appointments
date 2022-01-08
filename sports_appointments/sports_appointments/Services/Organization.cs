using sports_appointments.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace sports_appointments.Services
{
    public static class Organization
    {
        public static SQLiteAsyncConnection db;
        public static async Task InitializeOrg()
        {
            if (db != null)
                return;
            //Get absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");
            db = new SQLiteAsyncConnection(databasePath);

            await db.CreateTableAsync<User>();
            await db.CreateTableAsync<Court>();
            await db.CreateTableAsync<Appointment>();
        }
    }
}
