using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Shekhovtsova_backend.Models
{
    public class EnergyCard
    {
        public int EnergyCardID { get; set; }
        public int EnergyID { get; set; }
        [JsonIgnore]
        public Energy Energy { get; set; }
        
        public int CountryID { get; set; }
        //public Country Country { get; set; }
        public double Consumption { get; set; }
        public double Production { get; set; }

        public bool CanExport => Production > Consumption;

        public bool NeedImport => Production < Consumption;
    }
}
