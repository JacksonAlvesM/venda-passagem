

using SellBusTicket.Domain.Entities;

namespace SellBusTicket.Domain.Interfaces.Repositories
{
    public interface IRouteRepository
    {
        Task<IEnumerable<Route>> GetRoutesByFilterAsync(DateTime date, Guid originId, Guid destinationId);
    }
}
