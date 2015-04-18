using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GWSApp.Models
{
    public class ImportCustomer
    {
        /*object used to hold imported records from csv file. Column names match the csv. All properties are strings to avoid type errors  
         * with the csv reader, then the data is converted into the correct type when the respective objects are instantiated */
        public string InvNo { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string QtyRateA { get; set; }
        public string ASubTotal { get; set; }
        public string QtyRateB { get;  set; }
        public string BSubTotal { get; set; }
        public string QtyRateC { get; set; }
        public string CSubTotal { get; set; }
        public string QtyRateD { get; set; }
        public string DSubTotal { get; set; }
        public string QtyRateE { get; set; } 
        public string ESubTotal { get; set; }
        public string Total { get; set; }
        public string Arrears2008 { get; set; }
        public string Arrears2009 { get; set; }
        public string Arrears2010 { get; set; }
        public string Arrears2011 { get; set; }
        public string TotalArrears { get; set; }
        public string GrandTotal { get; set; }
        public string Paid { get; set; }
    }
}