using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GWSApp.Models
{
    public class Invoice
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int Year { get; set; }
        // usage per band, calculated when meter reading is entered into the system based on current rates
        public int QtyRateA { get; set; }
        public int QtyRateB { get; set; }
        public int QtyRateC { get; set; }
        public int QtyRateD { get; set; }
        public int QtyRateE { get; set; }
        // subtotals etc could be calculated dynamically, but it seems best to generate an invoice once and store it to reduce DB queries
        [DataType(DataType.Currency)]
        public float SubtotalA { get; set; }
        [DataType(DataType.Currency)]
        public float SubtotalB { get; set; }
        [DataType(DataType.Currency)]
        public float SubtotalC { get; set; }
        [DataType(DataType.Currency)]
        public float SubtotalD { get; set; }
        [DataType(DataType.Currency)]
        public float SubtotalE { get; set; }
        [DataType(DataType.Currency)]
        public float Total { get; set; }
        [DataType(DataType.Currency)]
        public float Arrears { get; set; }
        [DataType(DataType.Currency)]
        public float GrandTotal { get; set; }
        [DataType(DataType.Currency)]
        public float AmountPaid { get; set; }

        private bool Paid = false;

        public bool _Paid
        {
            get
            {
                return Paid;
            }
            set
            {
                Paid = value;
            }
        }

        public virtual Customer Customer { get; set; } 
    }
}