using sports_appointments.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sports_appointments.Services
{
    public static class AppointmentService
    {

        static async Task Init()
        {
            await Organization.InitializeOrg();
        }
        public static async Task AddAppointment(string date, int start, int end, int courtId, int userId)
        {
            await Init();
            if (Organization.db != null)
            {
                var newAppointment = new Appointment
                {
                    date = date,
                    start = start,
                    end = end,
                    courtId = courtId,
                    userId = userId,
                };
                await Organization.db.InsertAsync(newAppointment);
            }
        }
        public static async Task RemoveAppointment(int id)
        {
            await Init();
            await Organization.db.DeleteAsync<Appointment>(id);
        }
        public static async Task<IEnumerable<Appointment>> GetAppointments()
        {
            await Init();
            var appointments = await Organization.db.Table<Appointment>().ToListAsync();
            return appointments;
        }
        public static async Task<Appointment> GetAppointment(int appointmentId)
        {
            await Init();
            var appointments = await Organization.db.Table<Appointment>().ToListAsync();
            return appointments.Find(x => x.appointmentId == appointmentId);
        }
        public static async Task UpdateAppointment(Appointment appointment)
        {
            await Init();
            await Organization.db.UpdateAsync(appointment);
        }
    }
}

