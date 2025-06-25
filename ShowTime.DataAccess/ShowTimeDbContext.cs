using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Configurations;
using ShowTime.DataAccess.Models;

namespace ShowTime.DataAccess
{
    public class ShowTimeDbContext: DbContext   
    {
        public ShowTimeDbContext(DbContextOptions<ShowTimeDbContext> options)
            : base(options)
        {
        }
        public DbSet<Festival> Festivals { get; set; } = null!;
        public DbSet<Lineup> Lineups { get; set; } = null!;
        public DbSet<Artist> Artists { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShowTimeDbContext).Assembly);
            
            new ArtistConfiguration().Configure(modelBuilder.Entity<Artist>());
            new FestivalConfiguration().Configure(modelBuilder.Entity<Festival>());
            new LineupConfiguration().Configure(modelBuilder.Entity<Lineup>());
            new BookingConfiguration().Configure(modelBuilder.Entity<Booking>());
            new UserConfiguration().Configure(modelBuilder.Entity<User>());
        }
    }
}
