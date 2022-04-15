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
    public class EnergyCardsController : ControllerBase
    {
        private readonly AuthContext _context;

        public EnergyCardsController(AuthContext context)
        {
            _context = context;
        }

        // GET: api/EnergyCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnergyCard>>> GetEnergyCards()
        {
            return await _context.EnergyCards.ToListAsync();
        }

        // GET: api/EnergyCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EnergyCard>> GetEnergyCard(int id)
        {
            var energyCard = await _context.EnergyCards.FindAsync(id);

            if (energyCard == null)
            {
                return NotFound();
            }

            return energyCard;
        }

        // PUT: api/EnergyCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnergyCard(int id, EnergyCard energyCard)
        {
            if (id != energyCard.EnergyCardID)
            {
                return BadRequest();
            }

            _context.Entry(energyCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnergyCardExists(id))
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

        // POST: api/EnergyCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EnergyCard>> PostEnergyCard([FromForm]EnergyCard energyCard)
        {
            _context.EnergyCards.Add(energyCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnergyCard", new { id = energyCard.EnergyCardID }, energyCard);
        }

        // DELETE: api/EnergyCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnergyCard(int id)
        {
            var energyCard = await _context.EnergyCards.FindAsync(id);
            if (energyCard == null)
            {
                return NotFound();
            }

            _context.EnergyCards.Remove(energyCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnergyCardExists(int id)
        {
            return _context.EnergyCards.Any(e => e.EnergyCardID == id);
        }
    }
}
