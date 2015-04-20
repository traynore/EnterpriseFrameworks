using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using GWSApp.DAL;
using GWSApp.Models;

namespace GWSApp.App_Start
{
    public class AppdataConfig
    {
        private static GWSContext db = new GWSContext();

        // method called at application start by Global.asax
        public static RatesObject LoadRates()
        {
            // look for the rates settings in db. if none has been created, catch will return null which is handled in Global.Asax
            try
            {
                RatesObject rates = db.RatesObjects.First();
                return rates; 
                // Global.asax will store the rates values in key-value pairs in application state so no need to query rate table again
            }
            catch
            {
                return null;
            }
        }


    }
}