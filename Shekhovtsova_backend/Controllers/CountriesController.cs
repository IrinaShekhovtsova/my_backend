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

        // GET: api/Countries/ByName/Australia
        [HttpGet("byname/{name}")]
        public IActionResult GetCountryByName(string name)
        {
            var country = countryService.GetCountrybyName(name);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // GET: api/Countries/WithCards/3
        [HttpGet("withcards/{id}")]
        public IActionResult GetCountryWithCards(int id)
        {
            var countryWithCards = countryService.GetCountryWithCards(id);

            if (countryWithCards == null)
            {
                return NotFound();
            }

            return Ok(countryWithCards);
        }

        // GET: api/Countries/WithCards
        [HttpGet("withcards")]
        public List<CountryWithCards> GetCountriesWithCards()
        {
            return countryService.GetCountriesWithCards().ToList();
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
        [HttpGet("importers/{type}")]
        public List<CountryActivity> GetImporters(EnergyType type)
        {
            return countryService.GetImporters(type).ToList();
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




        //PUT: api/Countries/5
        
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCountry(int id, [FromForm]Country country)
        //{
        //    if (id != country.CountryID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(country).State = EntityState.Modified;

            
        //    await _context.SaveChangesAsync();
            

        //    return NoContent();
        //}

        // POST: api/Countries
        [HttpPost]
        public IActionResult PostCountry([FromForm]Country country)
        {
            if (!countryService.AddCountry(country)) 
                return NotFound();
            else return Ok(country);

        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            if (!countryService.DeleteCountry(id))
                return NotFound();
            else return Ok();
        }

        
    }
}
