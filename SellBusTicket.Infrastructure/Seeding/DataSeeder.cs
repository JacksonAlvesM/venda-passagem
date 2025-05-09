using SellBusTicket.Domain.Entities;
using SellBusTicket.Domain.Interfaces.Repositories;
using SellBusTicket.Domain.ValueObjects;
using SellBusTicket.Domain.Notification;
using System;
using System.Linq;

namespace SellBusTicket.Infrastructure.Seeding
{
    public class DataSeeder(IPlaceRepository placeRepository, IRouteRepository routeRepository, ISeatRepository seatRepository)
    {

        public void SeedData()
        {
            var notificationContext = new NotificationContext();  // Criação do NotificationContext

            var allRoutes = routeRepository.GetRoutesByFilterAsync(DateTime.Now, Guid.Empty, Guid.Empty).Result;
            if (allRoutes.Any())
                return;

            var rioId = Guid.Parse("d93a1e58-9e22-4b91-a13c-b402d2304a5d");
            var spId = Guid.Parse("e62a0d3f-dc1f-4f94-9c52-cf994cdfa0b6");

            var rio = new Place(rioId, new PlaceName("Rio de Janeiro - RJ"));
            var sp = new Place(spId, new PlaceName("São Paulo - SP"));

            placeRepository.AddAsync(rio);
            placeRepository.AddAsync(sp);

            var baseDate = DateTime.Now;

            var route1 = new Route(
                id: Guid.Parse("86fc8bca-31d9-40a2-8d77-b48d8a58a085"),
                originId: rio.Id,
                destinationId: sp.Id,
                departure: new DepartureDateTime(baseDate.AddHours(10), notificationContext),
                arrival: new ArrivalDateTime(baseDate.AddHours(15), notificationContext)
            );

            var route2 = new Route(
                id: Guid.Parse("3d9404af-dd9f-4abd-b7e8-5c3a21d0b213"),
                originId: rio.Id,
                destinationId: sp.Id,
                departure: new DepartureDateTime(baseDate.AddHours(15), notificationContext),
                arrival: new ArrivalDateTime(baseDate.AddHours(20), notificationContext)
            );

            routeRepository.AddAsync(route1);
            routeRepository.AddAsync(route2);

            foreach (var route in new[] { route1, route2 })
            {
                var seats = Enumerable.Range(1, 20).Select(num =>
                    new Seat(
                        id: Guid.NewGuid(),
                        routeId: route.Id,
                        number: new SeatNumber(num, notificationContext), 
                        available: new SeatAvailability(true)
                    )
                );

                foreach (var seat in seats)
                {
                    seatRepository.AddAsync(seat);
                }
            }
        }
    }
}
