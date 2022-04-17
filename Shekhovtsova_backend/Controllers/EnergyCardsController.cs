using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shekhovtsova_backend.Interfaces;
using Shekhovtsova_backend.Models;

namespace Shekhovtsova_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnergyCardsController : ControllerBase
    {
        private readonly AuthContext _context;
        private readonly IEnergyCard cardService;

        public EnergyCardsController(AuthContext context, IEnergyCard service)
        {
            _context = context;
            cardService = service;
        }

        // GET: api/EnergyCards
        [HttpGet]
        public List<EnergyCard> GetEnergyCards()
        {
            return cardService.GetEnergyCards().ToList();
        }

        // GET: api/EnergyCards/5
        [HttpGet("{id}")]
        public IActionResult GetEnergyCard(int id)
        {
            var energyCard = cardService.GetEnergyCard(id);

            if (energyCard == null)
            {
                return NotFound();
            }

            return Ok(energyCard);
        }

        // PUT: api/EnergyCards/5
        [HttpPut("{id}")]
        public IActionResult PutEnergyCard(int id, [FromForm]EnergyCard energyCard)
        {
            if (!cardService.UpdateEnergyCard(id, energyCard))
            {
                return BadRequest();
            }
            else return Ok(energyCard);
        }

        // POST: api/EnergyCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostEnergyCard([FromForm]EnergyCard energyCard)
        {
            if (!cardService.AddEnergyCard(energyCard))
                return NotFound();
            else return Ok(energyCard);
        }

        // DELETE: api/EnergyCards/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEnergyCard(int id)
        {
            if (!cardService.DeleteEnergyCard(id))
                return NotFound();
            else return Ok();
        }

        
    }
}
