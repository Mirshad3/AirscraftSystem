
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
                    Make = "Test Emp1",
                    Model = "Test@test.com",
                    Location = "Test1",
                    Registration = "Test@test.com",
                    DateAndTime = System.DateTime.Now
                },
                 new Aircraft()
                 {
                     Id = 2,
                     Make = "Test Emp1",
                     Model = "Test@test.com",
                     Location = "Test1",
                     Registration = "Test@test.com",
                     DateAndTime = System.DateTime.Now
                 },
                  new Aircraft()
                  {
                      Id = 3,
                      Make = "Test Emp1",
                      Model = "Test@test.com",
                      Location = "Test1",
                      Registration = "Test@test.com",
                      DateAndTime = System.DateTime.Now
                  });

             
            base.OnModelCreating(modelBuilder);
        }

    }
}