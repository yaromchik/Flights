using Project3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Flights.Domain.Entities;

namespace Project3.Data
{
    public class Entities: DbContext
    {
        public Entities(DbContextOptions<Entities> options) :base(options) 
        { 

        }
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Flight> Flights => Set<Flight>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductType> ProductTypes => Set<ProductType>();   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasKey(p => p.Email);

            modelBuilder.Entity<Flight>().Property(p => p.RemainingNumberOfSeats)
                .IsConcurrencyToken();

            modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);
            modelBuilder.Entity<Flight>().OwnsMany(f => f.Bookings);
        }
    }
}
