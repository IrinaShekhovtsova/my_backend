using Shekhovtsova_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shekhovtsova_backend.Dtos
{
    public class EnergyCardDto
    {
        public int EnergyCardID { get; set; }
        public EnergyType EnergyType { get; set; }
        public double Consumption { get; set; }
        public double Production { get; set; }
    }
}
