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
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
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

        [Authorize]
        // GET: api/Countries/WithCards
        [HttpGet("withcards")]
        public List<CountryWithCards> GetCountriesWithCards()
        {
            return countryService.GetCountriesWithCards().ToList();
        }

        [Authorize]
        // GET: api/Countries/Dirty
        [HttpGet("dirty")]
        public List<DirtyCountry> GetCountriesDirty()
        {
            return countryService.GetDirtyCountries().ToList();
        }

        [Authorize]
        // GET: api/Countries/consumptionstructure/4
        [HttpGet("consumptionstructure/{id}")]
        public IActionResult GetConsumptionStructure(int id)
        {
            if (countryService.CountryExists(id))
                return Ok(countryService.GetConsumptionStructure(id));
            else return NotFound();
        }

        [Authorize]
        // GET: api/Countries/Exporters/1
        [HttpGet("exporters/{type}")]
        public List<CountryActivity> GetExporters(EnergyType type)
        {
            return countryService.GetExporters(type).ToList();
            
        }

        [Authorize]
        // GET: api/Countries/Importers/1
        [HttpGet("importers/{type}")]
        public List<CountryActivity> GetImporters(EnergyType type)
        {
            return countryService.GetImporters(type).ToList();
        }
    


        // GET: api/Countries/5
        [HttpGet("{id}")]
        public IActionResult GetCountry(int id)
        {
            var country = countryService.GetCountry(id);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        [Authorize(Roles = "admin")]
        //PUT: api/Countries/5
        [HttpPut("{id}")]
        public IActionResult PutCountry(int id, [FromForm] Country country)
        {
            if (!countryService.UpdateCountry(country, id))
            {
                return BadRequest();
            }
            else return Ok(country);
        }


        [Authorize(Roles = "admin")]
        // POST: api/Countries
        [HttpPost]
        public IActionResult PostCountry([FromForm]Country country)
        {
            if (!countryService.AddCountry(country)) 
                return NotFound();
            else return Ok(country);

        }

        [Authorize(Roles = "admin")]
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
