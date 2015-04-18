using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GWSApp.Models
{
    public class Customer
    {
        public int ID { get; set; }
        [Display(Name = "Invoice No.")]
        public int? InvoiceNumber { get; set; }
        [Display(Name = "Surname")]
        public string LastName { get; set; }        
        [Display(Name = "Name")]
        public string FirstName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Join Date")]
        public DateTime? JoinDate { get; set; }
        [Display(Name = "Address 1")]
        public string Address1 { get; set; }
        [Display(Name = "Address 2")]
        public string Address2 { get; set; }
        [Display(Name = "Address 3")]
        public string Address3 { get; set; }
        [Display(Name = "Address 4")]
        public string Address4 { get; set; }
        public string Telephone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public virtual ICollection<MeterReading> MeterReadings { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
    }
}