using Microsoft.EntityFrameworkCore;
using System;

namespace SystemZarzadzaniaLotami.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
    }
}
