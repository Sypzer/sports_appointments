using sports_appointments.ViewModels;
using sports_appointments.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace sports_appointments
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
           
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
