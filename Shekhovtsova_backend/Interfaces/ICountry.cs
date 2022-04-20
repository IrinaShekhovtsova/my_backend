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

        public Country GetCountry(int id);

        public ConsumptionStruct GetConsumptionStructure(int id);

        public IEnumerable<CountryWithCards> GetCountriesWithCards();

        public CountryWithCards GetCountryWithCards(int id);

        public IEnumerable<CountryActivity> GetExporters(EnergyType type);

        public IEnumerable<CountryActivity> GetImporters(EnergyType e);

        //get dirty countries 

        public bool CountryExists(int id);

        public bool DeleteCountry(int id);

        public bool AddCountry(Country country);

        public Country GetCountrybyName(string name);

        public bool UpdateCountry(Country country, int id);

        public IEnumerable<DirtyCountry> GetDirtyCountries();


    }
}
