using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GWSApp.Models
{
    public class Note
    {
        [Display(Name = "Created At")]
        private DateTime Date = DateTime.Now;

        [Display(Name = "Created At")]
        public DateTime _Date
        {
            get
            {
                return Date;
            }
            set
            {
                Date = value;
            }
        }
        public int ID { get; set;}
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please Enter a Note Text")]
        [StringLength(160)]
        [DataType(DataType.MultilineText)]
        public string NoteText { get; set; }

        public virtual Customer Customer { get; set; }
    }
}