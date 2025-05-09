using SellBusTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.Interfaces.Repositories
{
    public interface ITripRepository
    { 
        Task AddAsync(Trip trip);
        Task<Trip?> GetByIdAsync(Guid id);
        Task<IEnumerable<Trip>> GetAllAsync();
    }
}
