using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinProyectoFinal.Models
{
    public class Resource
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }

        [Ignore]
        public ImageSource ImageSource => Image != null ? ImageSource.FromStream(() => new System.IO.MemoryStream(Image)) : null;
    }
}
