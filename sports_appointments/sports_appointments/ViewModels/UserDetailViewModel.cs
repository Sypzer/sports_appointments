using sports_appointments.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace sports_appointments.ViewModels
{
    [QueryProperty(nameof(UserId), nameof(UserId))]
    public class UserDetailViewModel :BaseViewModel
    {
        private int userId;
        private string username;
        private string password;
        private string email;
        private int role;
        public string Id { get; set; }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public int Role
        {
            get => role;
            set => SetProperty(ref role, value);
        }
        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
                LoadUserId(value);
            }
        }

        public async void LoadUserId(int userId)
        {
            try
            {
                var user = await UserService.GetUser(userId);
                UserId = user.userId;
                Username = user.username;
                Password = user.password;
                Email = user.email;
                Role = user.role;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
