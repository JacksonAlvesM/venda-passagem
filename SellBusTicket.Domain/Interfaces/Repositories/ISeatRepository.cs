using SellBusTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.Interfaces.Repositories
{
    public interface ISeatRepository
    {
        Task<IEnumerable<Seat>> GetSeatsByRouteIdAsync(Guid routeId);
        Task<Seat?> GetSeatByRouteAndNumberAsync(Guid routeId, int number);
        Task AddAsync(Seat seat);
        Task SaveChangesAsync();
    }
}
