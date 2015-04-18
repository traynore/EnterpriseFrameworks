using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GWSApp.Models
{
    public class RatesObject
    {
        /* defaults
        public int BandA = 115;
        public int BandB = 400;
        public int BandC = 800;
        public int BandD = 1500;
        // E = 1500+ 
        public double RateA = 0;
        public double RateB = 0.53;
        public double RateC = 0.48;
        public double RateD = 0.44;
        public double RateE = 0.4;
         * */

        public int ID { get; set; } //there should only be one rates object

        // hardcoded for 5 bands at present
        public string Description { get; set; }
        public int BandA { get; set; }
        public int BandB { get; set; }
        public int BandC { get; set; }
        public int BandD { get; set; }
        // E = 1500+ 

        [DataType(DataType.Currency)]
        public float RateA { get; set; }
        [DataType(DataType.Currency)]
        public float RateB { get; set; }
        [DataType(DataType.Currency)]
        public float RateC { get; set; }
        [DataType(DataType.Currency)]
        public float RateD { get; set; }
        [DataType(DataType.Currency)]
        public float RateE { get; set; }

    }
}