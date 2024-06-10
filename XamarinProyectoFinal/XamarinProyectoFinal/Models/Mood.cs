using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace XamarinProyectoFinal.Models
{
    public class Mood
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    }
}
