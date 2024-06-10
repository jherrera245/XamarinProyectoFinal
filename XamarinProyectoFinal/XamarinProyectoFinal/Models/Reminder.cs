using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinProyectoFinal.Models
{
    public class Reminder
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public TimeSpan Time { get; set; }
        public string Message { get; set; }
    }
}
