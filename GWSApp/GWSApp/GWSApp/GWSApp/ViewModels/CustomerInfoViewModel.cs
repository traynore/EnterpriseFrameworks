using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GWSApp.Models;


namespace GWSApp.ViewModels
{
    public class CustomerInfoViewModel
    {
        public List<Customer> customersList { get; set; }
        public List<MeterReading> meterReadingsList { get; set; }
        public List<Invoice> invoicesList { get; set; }
    }
}
