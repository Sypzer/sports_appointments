using System;
using SQLite;

namespace sports_appointments.Models
{
    public class Court
    {
        [PrimaryKey,AutoIncrement]
        public int courtId { get; set; }
        public String name { get; set; }
        public String address { get; set; }
        public int capacity { get; set; } 
    }

}
