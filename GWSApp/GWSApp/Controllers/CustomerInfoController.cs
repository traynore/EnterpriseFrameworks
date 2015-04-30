using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GWSApp.DAL;
using GWSApp.Models;
using GWSApp.ViewModels;

namespace GWSApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class CustomerInfoController : Controller
    {

        private GWSContext db = new GWSContext();

        // GET: CustomerInfo
        public ActionResult Index()
        {

            CustomerInfoViewModel vm = new CustomerInfoViewModel();
            vm.customersList = db.Customers.ToList();
            vm.invoicesList = db.Invoices.ToList();
            vm.meterReadingsList = db.MeterReadings.ToList();

            return View(vm);
        }
    }
}