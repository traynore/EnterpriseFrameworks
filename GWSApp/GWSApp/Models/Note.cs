using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GWSApp.Models
{
    public class Note
    {
        public int ID { get; set;}
        public int CustomerID { get; set; }
        public DateTime Date { get; set; }
        public string NoteText { get; set; }
    }
}