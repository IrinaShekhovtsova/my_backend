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

        public bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryID == id);
        }
    }
}
