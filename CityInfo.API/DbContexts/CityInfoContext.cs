using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options) 
        { 
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("Paris")
                {
                    Id = 1,
                    Description = "The one with the big tower"
                },
                new City("New York")
                {
                    Id = 2,
                    Description = " The one with the big statue of liberty"
                }
                );
            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Central Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "The most visited urban park in United States"
                },
                new PointOfInterest("Cathedral")
                {
                    Id = 2,
                    CityId = 2,
                    Description = "A 102-story skyscraper located in Midtown Manhattan"
                },
                new PointOfInterest("Empire State Building")
                {
                    Id= 3,
                    CityId = 2,
                    Description = "The tallest bulding of the new york"
                },
                new PointOfInterest("The Louvre")
                {
                    Id = 4,
                    CityId = 1,
                    Description = "The world's largest museum"
                }
                );
            base.OnModelCreating(modelBuilder);
        }

/*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite();
            base.OnConfiguring(optionsBuilder);
        }*/
    }
}
