using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shekhovtsova_backend.Models;
using Shekhovtsova_backend.Dtos;
using Shekhovtsova_backend.Services;
using Shekhovtsova_backend.Interfaces;
namespace Shekhovtsova_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly AuthContext _context;
        private readonly ICountry countryService;

        public CountriesController(AuthContext context, ICountry service)
        {
            _context = context;
            countryService = service;
        }

        // GET: api/Countries
        [HttpGet]
        public List<Country> GetCountryList()
        {
            return countryService.GetCountries().ToList();
        }

        // GET: api/Countries/WithCards
        [HttpGet("withcards")]
        public object GetCards()
        {
            var trial = _context.Countries
                .Select(c => new
                {
                    id = c.CountryID,
                    name = c.Name,
                    balance = c.EnergyBalance.Select(b => new
                    {
                        eid = b.EnergyID,
                        cons = b.Consumption,
                        prod = b.Production
                    }).ToList()

                })
                .ToList();
            return trial;
        }

        // GET: api/Countries/consumptionstructure/4
        [HttpGet("consumptionstructure/{id}")]
        public IActionResult GetConsumptionStructure(int id)
        {
            if (countryService.CountryExists(id))
                return Ok(countryService.GetConsumptionStructure(id));
            else return NotFound();
        }

        // GET: api/Countries/Exporters/1
        [HttpGet("exporters/{type}")]
        public List<CountryActivity> GetExporters(EnergyType type)
        {
            return countryService.GetExporters(type).ToList();
            
        }

        // GET: api/Countries/Importers/1
        [HttpGet("importers/{id}")]
        public List<CountryActivity> GetImporters(int id)
        {

            List<Country> countries = _context.Countries
                .Include(c => c.EnergyBalance)
                .ToList();

            Energy e = _context.Energies.Where(e => e.EnergyID == id).FirstOrDefault();

            List<CountryActivity> imp = countries.Where(c => c.IsImporter(e))
                .Select(c => new CountryActivity
                {
                    Name = c.Name,
                    Value = c.EnergyBalance.Where(ec => ec.EnergyID == id).Select(ec => ec.Consumption).FirstOrDefault()
                     - c.EnergyBalance.Where(ec => ec.EnergyID == id).Select(ec => ec.Production).FirstOrDefault()
                }
                ).OrderByDescending(c => c.Value).ToList();


            return imp;
        }




        // GET: api/Countries/dirty
        //[HttpGet("dirty")]
        //public object GetDirty()
        //{

        //    List<Country> countries = _context.Countries
        //        .Include(c => c.EnergyBalance)
        //        .Include(c => c.EnergyBalance.Select(b => b.Energy))
        //        .ToList();



        //    var em = countries.Select(c => c.EnergyBalance
        //    .Where(ec => !ec.Energy.IsGreen)
        //    .Select(ec => new
        //    {
        //        ener = ec.Energy.Type,
        //        cons = ec.Consumption
        //    }).ToList()).ToList();




        //    return em;
        //}


        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }




        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCountry(int id, Country country)
        //{
        //    if (id != country.CountryID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(country).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CountryExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Countries
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry([FromForm]Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.CountryID }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //private bool CountryExists(int id)
        //{
        //    return _context.Countries.Any(e => e.CountryID == id);
        //}
    }
}
