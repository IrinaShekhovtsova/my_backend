using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shekhovtsova_backend.Models;
using Shekhovtsova_backend.Dtos;

namespace Shekhovtsova_backend.Interfaces
{
    public interface ICountry
    {
        
        public IEnumerable<Country> GetCountries();

        //public Country GetCountry(int id);

        

        public ConsumptionStruct GetConsumptionStructure(int id);

        public IEnumerable<CountryActivity> GetExporters(EnergyType type);

        //public IEnumerable<CountryActivity> GetImporters(Energy e);

        //get dirty countries 

        public bool CountryExists(int id);

        //public bool DeleteCountry(int id);

        //public Country AddCountry(Country country);

        //public Country UpdateCountry(Country country, int id);


    }
}
