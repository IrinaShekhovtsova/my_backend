using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shekhovtsova_backend.Models
{
    public enum Region:int
        {
            NorthAmerica=1, SouthAmerica, Europe, CIS, MiddleEast, Africa, AsiaPacific
        };
    public class Country
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
        public Region Region { get; set; }

        [JsonIgnore]
        public List<EnergyCard> EnergyBalance { get; set; }

        //public bool IsExporter (Energy e)
        //{
        //   // EnergyBalance
        //   // .Where(e => !e.Energy.isGreen)
        //   // .Select(e => new { energy = e.Energy.Type, cons = e.Consumption });
        //    return EnergyBalance.FirstOrDefault(ec => ec.EnergyCardID == e.ID)?.CanExport == true;
        //}
        //public bool IsImporter (Energy e)
        //{
        //    return EnergyBalance.FirstOrDefault(ec => ec.EnergyCardID == e.ID)?.CanExport == false;
        //}

        public bool IsExporter(Energy e)
        {
            return EnergyBalance.FirstOrDefault(ec => ec.EnergyID == e.EnergyID).CanExport == true;
        }

        public bool IsImporter(Energy e)
        {
            return EnergyBalance.FirstOrDefault(ec => ec.EnergyID == e.EnergyID).NeedImport == true;
        }
        
    }
}
