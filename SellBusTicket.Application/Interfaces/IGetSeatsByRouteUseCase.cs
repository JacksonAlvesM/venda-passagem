using SellBusTicket.Application.DTOs.Seat;
using SellBusTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Application.Interfaces
{
    public interface IGetSeatsByRouteUseCase
    {
        Task<IEnumerable<SeatResponseDto>> ExecuteAsync(Guid routeId);
    }
}
