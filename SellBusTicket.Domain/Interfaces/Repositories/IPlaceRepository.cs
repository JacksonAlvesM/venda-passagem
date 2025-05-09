using SellBusTicket.Domain.Entities;

namespace SellBusTicket.Domain.Interfaces.Repositories
{
    public interface IPlaceRepository
    {
        Task<IEnumerable<Place>> GetAllAsync();
        public Task AddAsync(Place place);
        Task<Place?> GetByIdAsync(Guid id);

    }
}
