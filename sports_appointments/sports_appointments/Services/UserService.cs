using sports_appointments.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace sports_appointments.Services
{
    public static class UserService
    {
        static User loggedUser;
        static async Task Init()
        {
            await Organization.InitializeOrg();
        }
        public static async Task AddUser(string username, string password, string email, int role)
        {
            await Init();
            if (Organization.db != null)
            {
                var newUser= new User
                {
                    username = username,
                    password = password,
                    email = email,
                    role = role
                };
                await Organization.db.InsertAsync(newUser);
            }
        }

        public static async Task<string> Login(string username,string password)
        {
            var users = await GetUsers();
            foreach(User user in users)
            {
                if(user.username == username && user.password == password)
                {
                    loggedUser = user;
                    return "Logged in succesfully";
                }
            }
            return "Login failed.";
        }
        public static async Task RemoveUser(int id)
        {
            await Init();
            await Organization.db.DeleteAsync<Court>(id);
        }
        public static async Task<IEnumerable<User>> GetUsers()
        {
            await Init();
            var users= await Organization.db.Table<User>().ToListAsync();
            return users;
        }
        public static async Task<User> GetUser(int userId)
        {
            await Init();
            var courts = await Organization.db.Table<User>().ToListAsync();
            return courts.Find(x => x.userId == userId);
        }
        public static async Task UpdateUser(User user)
        {
            await Init();
            await Organization.db.UpdateAsync(user);
        }
    }
}
