using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shekhovtsova_backend.Dtos
{
    public class CountryWithCards
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
        public IEnumerable<EnergyCardDto> Balance { get; set; }
    }
}
