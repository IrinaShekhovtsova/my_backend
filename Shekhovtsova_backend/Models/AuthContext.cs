using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Shekhovtsova_backend.Models
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options)
            : base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Energy> Energies { get; set; }
        public DbSet<EnergyCard> EnergyCards { get; set; }
        public DbSet<Country> Countries { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Energy>().HasData(new Energy { EnergyID = 1, Type = EnergyType.Oil, EcologyDamage = 6 });
            modelBuilder.Entity<Energy>().HasData(new Energy { EnergyID = 2, Type = EnergyType.Gas, EcologyDamage = 3 });
            modelBuilder.Entity<Energy>().HasData(new Energy { EnergyID = 3, Type = EnergyType.Coal, EcologyDamage = 10 });
        }
    }
}
