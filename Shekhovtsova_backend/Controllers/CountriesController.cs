using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shekhovtsova_backend.Models;

namespace Shekhovtsova_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly AuthContext _context;

        public CountriesController(AuthContext context)
        {
            _context = context;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
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

        // GET: api/Countries/Exporters/1
        [HttpGet("exporters/{id}")]
        public List<CountryActivity> GetExporters(int id)
        {

            List<Country> countries = _context.Countries
                .Include(c => c.EnergyBalance)
                .ToList();

            Energy e = _context.Energies.Where(e => e.EnergyID == id).FirstOrDefault();

            List<CountryActivity> exp = countries.Where(c => c.IsExporter(e))
                .Select(c => new CountryActivity
                {
                    Name = c.Name,
                    Value = c.EnergyBalance.Where(ec => ec.EnergyID == id).Select(ec => ec.Production).FirstOrDefault()
                     - c.EnergyBalance.Where(ec => ec.EnergyID == id).Select(ec => ec.Consumption).FirstOrDefault()
                }
                ).OrderByDescending(c => c.Value).ToList();


            return exp;
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country country)
        {
            if (id != country.CountryID)
            {
                return BadRequest();
            }

            _context.Entry(country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

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

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryID == id);
        }
    }
}
