
using AircraftSystem.Models;
using Microsoft.EntityFrameworkCore;  
  
namespace AircraftSystem.Context
{
    public class AircraftContext : DbContext
    {
        public AircraftContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Aircraft> Aircrafts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aircraft>()
                .HasData(
                new Aircraft()
                {
                    Id = 1,
                    Make = "Beeing",
                    Model = "777-300ER",
                    Location = "London Gatwick",
                    Registration = "G-RNAC",
                    DateAndTime = System.DateTime.Now
                },
                 new Aircraft()
                 {
                     Id = 2,
                   Make = "Teeing",
                    Model = "345-890ER",
                    Location = "Colombo Lanka",
                    Registration = "G-MRAC",
                     DateAndTime = System.DateTime.Now
                 },
                  new Aircraft()
                  {
                      Id = 3,
                      Make = "Gamin",
                    Model = "879-300GM",
                    Location = "India Delli",
                    Registration = "I-DELL",
                      DateAndTime = System.DateTime.Now
                  });

             
            base.OnModelCreating(modelBuilder);
        }

    }
}
