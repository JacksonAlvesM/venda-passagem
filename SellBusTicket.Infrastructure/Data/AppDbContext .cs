using Microsoft.EntityFrameworkCore;
using SellBusTicket.Domain.Entities;

namespace SellBusTicket.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Route> Routes => Set<Route>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<Place> Places => Set<Place>();
        public DbSet<Trip> Trips => Set<Trip>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.OriginId);
                entity.Property(e => e.DestinationId);

                entity.OwnsOne(e => e.Departure, owned =>
                {
                    owned.Property(d => d.Value).HasColumnName("DepartureDateTime");
                });

                entity.OwnsOne(e => e.Arrival, owned =>
                {
                    owned.Property(a => a.Value).HasColumnName("ArrivalDateTime");
                });
            });

            modelBuilder.Entity<Seat>(builder =>
            {
                builder.HasKey(e => e.Id);
                builder.Property(e => e.RouteId);

                builder.OwnsOne(s => s.Number, p =>
                {
                    p.Property(x => x.Value).HasColumnName("SeatNumber");
                });
                builder.OwnsOne(s => s.Available, p =>
                {
                    p.Property(x => x.Value).HasColumnName("IisAvailable");
                });
            });

            modelBuilder.Entity<Place>(builder =>
            {
                builder.HasKey(e => e.Id);

                builder.OwnsOne(s => s.Name, p =>
                {
                    p.Property(x => x.Value).HasColumnName("PlaceName");
                });
            });

            modelBuilder.Entity<Trip>(builder =>
            {
                builder.HasKey(e => e.Id);
                builder.Property(e => e.RouteId);

                builder.OwnsOne(s => s.Name, p =>
                {
                    p.Property(x => x.Value).HasColumnName("PassengerName");
                });
                builder.OwnsOne(s => s.Cpf, p =>
                {
                    p.Property(x => x.Value).HasColumnName("Cpf");
                });
                builder.OwnsOne(s => s.Seat, p =>
                {
                    p.Property(x => x.Value).HasColumnName("SeatNumber");
                });
            });
        }
    }
}

