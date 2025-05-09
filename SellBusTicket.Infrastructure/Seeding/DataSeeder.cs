using SellBusTicket.Domain.Entities;
using SellBusTicket.Domain.ValueObjects;
using SellBusTicket.Infrastructure.Data;
using System;
using System.Linq;

namespace SellBusTicket.Infrastructure.Seeding
{
    public class DataSeeder(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public void SeedData()
        {
            if (_context.Routes.Any() || _context.Places.Any())
                return;

            var rioId = Guid.Parse("d93a1e58-9e22-4b91-a13c-b402d2304a5d");
            var spId = Guid.Parse("e62a0d3f-dc1f-4f94-9c52-cf994cdfa0b6");

            var rio = new Place(rioId, new PlaceName("Rio de Janeiro - RJ"));
            var sp = new Place(spId, new PlaceName("São Paulo - SP"));

            _context.Places.AddRange(rio, sp);

            var baseDate = new DateTime(2025, 6, 25);

            var route1 = new Route(
                id: Guid.NewGuid(),
                originId: rio.Id,
                destinationId: sp.Id,
                departure: new DepartureDateTime(baseDate.AddHours(10)),
                arrival: new ArrivalDateTime(baseDate.AddHours(15))
            );

            var route2 = new Route(
                id: Guid.NewGuid(),
                originId: rio.Id,
                destinationId: sp.Id,
                departure: new DepartureDateTime(baseDate.AddHours(15)),
                arrival: new ArrivalDateTime(baseDate.AddHours(20))
            );

            _context.Routes.AddRange(route1, route2);

            foreach (var route in new[] { route1, route2 })
            {
                var seats = Enumerable.Range(1, 20).Select(num =>
                    new Seat(
                        id: Guid.NewGuid(),
                        routeId: route.Id,
                        number: new SeatNumber(num),
                        available: new SeatAvailability(true)
                    )
                );

                _context.Seats.AddRange(seats);
            }

            _context.SaveChanges();
        }
    }
}
