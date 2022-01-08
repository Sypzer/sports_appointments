using System;
using SQLite;

namespace sports_appointments.Models
{
    public class Appointment
    {
        [PrimaryKey, AutoIncrement]
        public int appointmentId { get; set; }
        public string date { get; set; }
        public int start { get; set; }
        public int end { get; set; }
        public int courtId { get; set; }
        public int userId { get; set; }
    }
}
