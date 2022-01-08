using System;
using SQLite;

namespace sports_appointments.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int userId { get; set; }
        public String username{ get; set; }
        public String password { get; set; }
        public String email { get; set; }
        public int role { get; set; }
    }
}
