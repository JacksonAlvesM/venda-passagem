using SellBusTicket.Domain.Entities;
using SellBusTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Infrastructure.Repositories.InMemory
{
    public class InMemoryPlaceRepository : IPlaceRepository
    {
        private readonly List<Place> _places = new();

        public Task<IEnumerable<Place>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Place>>(_places);
        }

    }
}
