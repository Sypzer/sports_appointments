using DevExpress.Data.XtraReports.Native;
using MvvmHelpers.Commands;
using sports_appointments.Models;
using sports_appointments.Services;
using sports_appointments.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace sports_appointments.ViewModels
{
    public class CourtViewModel : BaseViewModel
    {
        private Court _selectedCourt;

        public ObservableRangeCollection<Court> Courts { get; }

        public AsyncCommand LoadCourtsCommand { get; }
        public AsyncCommand AddCourtCommand { get; }
        public AsyncCommand<Court> CourtTapped { get; }
        public AsyncCommand<Court> RemoveCourtCommand { get; }
        public AsyncCommand<Court> UpdateCourtCommand { get; }

        public CourtViewModel()
        {
            Title = "Browse Courts";
            Courts = new ObservableRangeCollection<Court>();
            
            LoadCourtsCommand = new AsyncCommand(async () => await ExecuteLoadCourtsCommand());

            CourtTapped = new AsyncCommand<Court>(OnCourtSelected);

            AddCourtCommand = new AsyncCommand(OnAddCourt);

            RemoveCourtCommand = new AsyncCommand<Court>(OnRemoveCourt);

            UpdateCourtCommand = new AsyncCommand<Court>(OnUpdateCourt);
        }

        async Task OnUpdateCourt(Court court)
        {
            await CourtService.UpdateCourt(court);
        }

        async Task OnRemoveCourt(Court court)
        {
            IsBusy = true;
            try
            {
                await CourtService.RemoveCourt(court.courtId);
                await ExecuteLoadCourtsCommand();
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

        async Task ExecuteLoadCourtsCommand()
        {
            IsBusy = true;

            try
            {
                Courts.Clear();
                var courts = await CourtService.GetCourts();
                Courts.AddRange(courts);
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

        async Task OnAddCourt()
        {
            //await Shell.Current.GoToAsync(nameof(NewCourtPage));
            var name = await App.Current.MainPage.DisplayPromptAsync("Name", "Name of Court");
            var address= await App.Current.MainPage.DisplayPromptAsync("Address", "Address of Court");
            var capacity= await App.Current.MainPage.DisplayPromptAsync("Capacity", "Capacity of Court");
            await CourtService.AddCourt(name, address, int.Parse(capacity));
            await ExecuteLoadCourtsCommand();
        }
        
        public Court SelectedCourt
        {
            get => _selectedCourt;
            set
            {
                SetProperty(ref _selectedCourt, value);
                OnCourtSelected(value);
            }
        }

        async Task OnCourtSelected(Court court)
        {
            if (court == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            Routing.RegisterRoute($"{nameof(CourtDetailPage)}?{nameof(CourtDetailViewModel.CourtId)}={court.courtId}", typeof(CourtDetailPage));
            await Shell.Current.GoToAsync($"{nameof(CourtDetailPage)}?{nameof(CourtDetailViewModel.CourtId)}={court.courtId}");
        }
    }
}
