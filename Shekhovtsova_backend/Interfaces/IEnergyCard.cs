using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shekhovtsova_backend.Models;
using Shekhovtsova_backend.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Shekhovtsova_backend.Interfaces
{
    public interface IEnergyCard
    {
        public bool AddEnergyCard(EnergyCard energyCard);
        public bool UpdateEnergyCard(int id, EnergyCard energyCard);

        public EnergyCard GetEnergyCard(int id);
        public bool EnergyCardExists(int id);

        public bool DeleteEnergyCard(int id);

        public IEnumerable<EnergyCard> GetEnergyCards();
    }
}
