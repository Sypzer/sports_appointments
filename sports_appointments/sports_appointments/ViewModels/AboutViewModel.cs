using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace sports_appointments.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Basketball Courts";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://www.nba.com/"));
        }

        public ICommand OpenWebCommand { get; }
    }
}