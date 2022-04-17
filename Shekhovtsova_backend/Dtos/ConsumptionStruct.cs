using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shekhovtsova_backend.Dtos
{
    public class ConsumptionStruct
    {
        public double TotalConsumption { get; set; }
        public double Oil { get; set; }

        public double Gas { get; set; }
        public double Coal { get; set; }
    }
}
