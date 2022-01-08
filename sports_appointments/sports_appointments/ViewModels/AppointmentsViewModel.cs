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
    public class AppointmentsViewModel : BaseViewModel
    {
        private Appointment _selectedAppointment;

        public ObservableRangeCollection<Appointment> Appointments{ get; }

        public AsyncCommand LoadAppointmentsCommand { get; }
        public AsyncCommand AddAppointmentCommand { get; }
        public AsyncCommand<Appointment> AppointmentTapped { get; }
        public AsyncCommand<Appointment> RemoveAppointmentCommand { get; }
        public AsyncCommand<Appointment> UpdateAppointmentCommand { get; }

        public AppointmentsViewModel()
        {
            Title = "Browse Appointments";

            Appointments = new ObservableRangeCollection<Appointment>();

            LoadAppointmentsCommand = new AsyncCommand(async () => await ExecuteLoadAppointmentsCommand());

            AppointmentTapped = new AsyncCommand<Appointment>(OnAppointmentSelected);

            AddAppointmentCommand = new AsyncCommand(OnAddAppointment);

            RemoveAppointmentCommand = new AsyncCommand<Appointment>(OnRemoveAppointment);

            UpdateAppointmentCommand = new AsyncCommand<Appointment>(OnUpdateAppointment);
        }

        async Task OnUpdateAppointment(Appointment appointment)
        {
            await AppointmentService.UpdateAppointment(appointment);
        }

        async Task OnRemoveAppointment(Appointment appointment)
        {
            IsBusy = true;
            try
            {
                await AppointmentService.RemoveAppointment(appointment.appointmentId);
                await ExecuteLoadAppointmentsCommand();
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

        async Task ExecuteLoadAppointmentsCommand()
        {
            IsBusy = true;

            try
            {
                Appointments.Clear();
                var appointments = await AppointmentService.GetAppointments();
                Appointments.AddRange(appointments);
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

        async Task OnAddAppointment()
        {
            //await Shell.Current.GoToAsync(nameof(NewCourtPage));
            var date = await App.Current.MainPage.DisplayPromptAsync("Date", "Name of Appointment");
            var start = await App.Current.MainPage.DisplayPromptAsync("Start hour", "Start hour of Appointment");
            var end = await App.Current.MainPage.DisplayPromptAsync("End hour", "End hour of Appointment");
            var courtId = await App.Current.MainPage.DisplayPromptAsync("Court ID", "Court ID of Appointment");
            var userId = await App.Current.MainPage.DisplayPromptAsync("User ID", "User ID of Appointment");
            await AppointmentService.AddAppointment(date, int.Parse(start), int.Parse(end), int.Parse(courtId), int.Parse(userId));
            await ExecuteLoadAppointmentsCommand();
        }

        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                SetProperty(ref _selectedAppointment, value);
                OnAppointmentSelected(value);
            }
        }

        async Task OnAppointmentSelected(Appointment appointment)
        {
            if (appointment == null)
                return;
            // This will push the ItemDetailPage onto the navigation stack
            //            Routing.RegisterRoute($"{nameof(AppointmentDetailPage)}?{nameof(AppointmentDetailsViewModel.AppointmentId)}={appointment.appointmentId}");
            //await Shell.Current.GoToAsync($"{nameof(CourtDetailPage)}?{nameof(CourtDetailViewModel.CourtId)}={court.courtId}");
            Routing.RegisterRoute($"{nameof(AppointmentDetailPage)}?{nameof(AppointmentDetailsViewModel.AppointmentId)}={appointment.appointmentId}", typeof(AppointmentDetailPage));

            await Shell.Current.GoToAsync($"{nameof(AppointmentDetailPage)}?{nameof(AppointmentDetailsViewModel.AppointmentId)}={appointment.appointmentId}");
        }
    }
}

