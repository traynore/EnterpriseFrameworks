using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using GWSApp.Models;

namespace GWSApp.DAL
{
    public class GWSContext : DbContext
    {

        public GWSContext()
            : base("GWSContext")
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<RatesObject> RatesObjects { get; set; }

    }
}