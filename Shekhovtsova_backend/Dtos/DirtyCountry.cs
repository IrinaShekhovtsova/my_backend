using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shekhovtsova_backend.Dtos
{
    public class DirtyCountry
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
        public IEnumerable<DirtyCard> Balance { get; set; }

        public double TotalDirtyConsumption { get; set; }
    }
}
