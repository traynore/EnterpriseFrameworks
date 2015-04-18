using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GWSApp.Models
{
    public class MeterReading
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int Year { get; set; }
        public int Quantity { get; set; }

        public virtual Customer Customer { get; set; } 
    }
}