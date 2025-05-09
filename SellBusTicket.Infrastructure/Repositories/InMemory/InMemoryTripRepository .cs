using SellBusTicket.Domain.Entities;
using SellBusTicket.Domain.Interfaces.Repositories;

namespace SellBusTicket.Infrastructure.Repositories.InMemory
{
    public class InMemoryTripRepository : ITripRepository
    {
        private readonly List<Trip> _trips = new();

        public Task AddAsync(Trip trip)
        {
            _trips.Add(trip);
            return Task.CompletedTask;
        }

        public Task<Trip?> GetByIdAsync(Guid id)
        {
            var trip = _trips.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(trip);
        }

        public Task<IEnumerable<Trip>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Trip>>(_trips);
        }
    }
}
