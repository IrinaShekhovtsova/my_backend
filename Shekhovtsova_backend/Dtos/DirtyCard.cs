using Shekhovtsova_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shekhovtsova_backend.Dtos
{
    public class DirtyCard
    {
        public EnergyType EnergyType { get; set; }
        public double DirtyConsumption { get; set; }
    }
}
