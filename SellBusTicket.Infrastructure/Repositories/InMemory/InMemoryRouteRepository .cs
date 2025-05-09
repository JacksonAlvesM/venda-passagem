using SellBusTicket.Domain.Entities;
using SellBusTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Infrastructure.Repositories.InMemory
{
    public class InMemoryRouteRepository : IRouteRepository
    {
        private readonly List<Route> _routes = new();

        public Task<IEnumerable<Route>> GetRoutesByFilterAsync(DateTime date, Guid originId, Guid destinationId)
        {
            var filtered = _routes
                .Where(r => r.OriginId == originId &&
                            r.DestinationId == destinationId &&
                            r.Departure.Value.Date >= date.Date);
            return Task.FromResult<IEnumerable<Route>>(filtered);
        }

        public Task<Route?> GetByIdAsync(Guid id)
        {
            var route = _routes.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(route);
        }
        public Task AddAsync(Route route)
        {
            _routes.Add(route);
            return Task.CompletedTask;
        }

    }
}
