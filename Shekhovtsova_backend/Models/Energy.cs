using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shekhovtsova_backend.Models
{
    public enum EnergyType:int
    {
        Oil=0,
        Gas,
        Coal
    }
    public class Energy
    {
        public int EnergyID {get;set;}

       
        public EnergyType Type { get; set; }
        public int EcologyDamage { get; set; }
        public bool IsGreen => EcologyDamage < 4;
    }
}
