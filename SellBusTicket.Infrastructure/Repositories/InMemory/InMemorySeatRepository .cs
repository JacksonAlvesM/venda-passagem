using SellBusTicket.Domain.Entities;
using SellBusTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Infrastructure.Repositories.InMemory
{
    public class InMemorySeatRepository : ISeatRepository
    {
        private readonly List<Seat> _seats = new();

        public Task<IEnumerable<Seat>> GetSeatsByRouteIdAsync(Guid routeId)
        {
            var filtered = _seats.Where(s => s.RouteId == routeId);
            return Task.FromResult<IEnumerable<Seat>>(filtered);
        }

        public Task<Seat?> GetSeatByRouteAndNumberAsync(Guid routeId, int number)
        {
            var seat = _seats.FirstOrDefault(s => s.RouteId == routeId && s.Number.Value == number);
            return Task.FromResult(seat);
        }

        public Task AddAsync(Seat seat)
        {
            _seats.Add(seat);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync()
        {
            return Task.CompletedTask;
        }
    }
}
