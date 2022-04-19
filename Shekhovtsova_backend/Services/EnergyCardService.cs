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
    public class EnergyCardService : IEnergyCard
    {
        private readonly AuthContext _context;

        public EnergyCardService(AuthContext context)
        {
            _context = context;
        }

        public bool AddEnergyCard(EnergyCard energyCard)
        {
            if(_context.Countries.Find(energyCard.CountryID) is not null)
                _context.EnergyCards.Add(energyCard);
            return _context.SaveChanges() > 0;
        }

        public bool EnergyCardExists(int id)
        {
            return _context.EnergyCards.Any(e => e.EnergyCardID == id);
        }

        public bool UpdateEnergyCard(int id, EnergyCard energyCard)
        {
            if (id != energyCard.EnergyCardID) return false;

            var ec = GetEnergyCard(id);
            if (ec is null) return false;

            ec.EnergyID = energyCard.EnergyID;
            ec.CountryID = energyCard.CountryID;
            ec.Consumption = energyCard.Consumption;
            ec.Production = energyCard.Production;

            return _context.SaveChanges() > 0;
        }

        public EnergyCard GetEnergyCard(int id)
        {
            return _context.EnergyCards.Find(id);
        }

        public bool DeleteEnergyCard(int id)
        {
            var ec = GetEnergyCard(id);
            if (ec is not null) _context.Remove(ec);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<EnergyCard> GetEnergyCards()
        {
            return _context.EnergyCards;
        }
    }
}
