using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GWSApp.Models
{
    public class RatesObject
    {

        public int ID { get; set; } 

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