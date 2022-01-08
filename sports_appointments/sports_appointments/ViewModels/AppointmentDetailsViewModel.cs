using sports_appointments.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace sports_appointments.ViewModels
{
    [QueryProperty(nameof(AppointmentId), nameof(AppointmentId))]
    public class AppointmentDetailsViewModel :BaseViewModel
    {
        private int appointmentId;
        private string date;
        private int start;
        private int end;
        private int courtId;
        private int userId;
        public int Id { get; set; }

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public int Start
        {
            get => start;
            set => SetProperty(ref start, value);
        }
        public int End
        {
            get => end;
            set => SetProperty(ref end, value);
        }
        public int CourtId
        {
            get => courtId;
            set => SetProperty(ref courtId, value);
        }
        public int UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }
        public int AppointmentId
        {
            get
            {
                return appointmentId;
            }
            set
            {
                appointmentId  = value;
                LoadAppointmentId(value);
            }
        }

        public async void LoadAppointmentId(int appointmentId)
        {
            try
            {
                var appointment = await AppointmentService.GetAppointment(appointmentId);
                Id = appointment.appointmentId;
                Date = appointment.date;
                Start = appointment.start;
                End = appointment.end;
                CourtId = appointment.courtId;
                UserId = appointment.userId;

            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}

