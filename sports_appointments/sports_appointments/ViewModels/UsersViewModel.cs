using DevExpress.Data.XtraReports.Native;
using MvvmHelpers.Commands;
using sports_appointments.Models;
using sports_appointments.Services;
using sports_appointments.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace sports_appointments.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private User _selectedUser;
        public ObservableRangeCollection<User> Users { get; }

        public AsyncCommand LoadUsersCommand { get; }
        public AsyncCommand AddUserCommand { get; }
        public AsyncCommand<User> UserTapped { get; }
        public AsyncCommand<User> RemoveUserCommand { get; }
        public AsyncCommand<User> UpdateUserCommand { get; }

        public UsersViewModel()
        {
            Title = "Users dashboard";
            Users = new ObservableRangeCollection<User>();

            LoadUsersCommand = new AsyncCommand(async () => await ExecuteLoadUsersCommand());

            UserTapped = new AsyncCommand<User>(OnUserSelected);

            AddUserCommand = new AsyncCommand(OnAddUser);

            RemoveUserCommand = new AsyncCommand<User>(OnRemoveUser);

            UpdateUserCommand = new AsyncCommand<User>(OnUpdateUser);
        }

        async Task OnUpdateUser(User user)
        {
            await UserService.UpdateUser(user);
        }

        async Task OnRemoveUser(User user)
        {
            IsBusy = true;
            try
            {
                await UserService.RemoveUser(user.userId);
                await ExecuteLoadUsersCommand();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadUsersCommand()
        {
            IsBusy = true;

            try
            {
                Users.Clear();
                var users = await UserService.GetUsers();
                Users.AddRange(users);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task OnAddUser()
        {
            //await Shell.Current.GoToAsync(nameof(NewCourtPage));
            var username = await App.Current.MainPage.DisplayPromptAsync("Username", "Username of User");
            var password = await App.Current.MainPage.DisplayPromptAsync("Password", "Password of User");
            var email = await App.Current.MainPage.DisplayPromptAsync("Email", "Email of User");
            var role = await App.Current.MainPage.DisplayPromptAsync("Role", "Role of User");
            await UserService.AddUser(username, password, email, int.Parse(role));
            await ExecuteLoadUsersCommand();
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser , value);
                OnUserSelected(value);
            }
        }

        async Task OnUserSelected(User user)
        {
            if (user == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            Routing.RegisterRoute($"{nameof(UserDetailPage)}?{nameof(UserDetailViewModel.UserId)}={user.userId}", typeof(UserDetailPage));
            await Shell.Current.GoToAsync($"{nameof(UserDetailPage)}?{nameof(UserDetailViewModel.UserId)}={user.userId}");
        }
    }
}
