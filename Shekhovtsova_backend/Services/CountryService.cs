using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shekhovtsova_backend.Models;
using Shekhovtsova_backend.Dtos;
using Shekhovtsova_backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Shekhovtsova_backend.Services
{
    public class CountryService : ICountry
    {
        private readonly AuthContext _context;

        public CountryService(AuthContext context)
        {
            _context = context;
        }

        public IEnumerable<Country> GetCountries()
        {
            return _context.Countries;
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Find(id);
        }

        public CountryWithCards GetCountryWithCards(int id)
        {
            if (!CountryExists(id)) return null;

            CountryWithCards country = _context.Countries.Where(c => c.CountryID == id)
                .Select(c => new CountryWithCards
                {
                    CountryID = c.CountryID,
                    Name = c.Name,
                    Balance = c.EnergyBalance.Select(ec => new EnergyCardDto
                    {
                        EnergyCardID = ec.EnergyCardID,
                        EnergyType = ec.Energy.Type,
                        Consumption = ec.Consumption,
                        Production = ec.Production
                    })

                }).FirstOrDefault();

            return country;
        }

        public IEnumerable<CountryWithCards> GetCountriesWithCards()
        {
            IEnumerable<CountryWithCards> countries_with_cards = _context.Countries
                    .Select(c => new CountryWithCards
                    {
                        CountryID = c.CountryID,
                        Name = c.Name,
                        Balance = c.EnergyBalance.Select(ec => new EnergyCardDto
                        {
                            EnergyCardID = ec.EnergyCardID,
                            EnergyType = ec.Energy.Type,
                            Consumption = ec.Consumption,
                            Production = ec.Production
                        })

                    });
            return countries_with_cards;
        }
        public ConsumptionStruct GetConsumptionStructure(int id)
        {

            ConsumptionStruct str = _context.Countries
                .Where(c => c.CountryID == id)
                .Select(c => c.EnergyBalance)
                .Select(ec => new ConsumptionStruct
                {
                    TotalConsumption = ec.Sum(ec => ec.Consumption),
                    Oil = ec.Where(ec => ec.EnergyID == 1).Select(ec => ec.Consumption).FirstOrDefault(),
                    Gas = ec.Where(ec => ec.EnergyID == 2).Select(ec => ec.Consumption).FirstOrDefault(),
                    Coal = ec.Where(ec => ec.EnergyID == 3).Select(ec => ec.Consumption).FirstOrDefault()
                }).FirstOrDefault();

            return str;
        }

        public Country GetCountrybyName(string name)
        {
            return _context.Countries.Where(c => c.Name == name).FirstOrDefault();
        }

        public IEnumerable<CountryActivity> GetExporters(EnergyType type)
        {
            List<Country> countries = _context.Countries
                .Include(c => c.EnergyBalance)
                .ToList();

            Energy e = _context.Energies.Where(e => e.Type == type).FirstOrDefault();

            IEnumerable<CountryActivity> exp = countries.Where(c => c.IsExporter(e))
                .Select(c => new CountryActivity
                {
                    Name = c.Name,
                    Value = c.EnergyBalance.Where(ec => ec.EnergyID == e.EnergyID).Select(ec => ec.Production).FirstOrDefault()
                     - c.EnergyBalance.Where(ec => ec.EnergyID == e.EnergyID).Select(ec => ec.Consumption).FirstOrDefault()
                }
                ).OrderByDescending(c => c.Value);
            return exp;

        }

        public IEnumerable<CountryActivity> GetImporters(EnergyType type)
        {
            List<Country> countries = _context.Countries
                .Include(c => c.EnergyBalance)
                .ToList();

            Energy e = _context.Energies.Where(e => e.Type == type).FirstOrDefault();

            IEnumerable<CountryActivity> imp = countries.Where(c => c.IsImporter(e))
                .Select(c => new CountryActivity
                {
                    Name = c.Name,
                    Value = c.EnergyBalance.Where(ec => ec.EnergyID == e.EnergyID).Select(ec => ec.Consumption).FirstOrDefault()
                     - c.EnergyBalance.Where(ec => ec.EnergyID == e.EnergyID).Select(ec => ec.Production).FirstOrDefault()
                }
                ).OrderByDescending(c => c.Value);
            return imp;

        }

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryID == id);
        }

        public bool DeleteCountry(int id)
        {
            var country = GetCountry(id);
            if (country is not null) _context.Remove(country);
            return _context.SaveChanges() > 0;
        }

        public bool AddCountry(Country country)
        { 
            _context.Countries.Add(country);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateCountry(Country country, int id)
        {
            if (id != country.CountryID) return false;

            var c = GetCountry(id);
            if (c is null) return false;

            c.Name = country.Name;
            c.Region = country.Region;

            return _context.SaveChanges() > 0;
        }
    }
}
