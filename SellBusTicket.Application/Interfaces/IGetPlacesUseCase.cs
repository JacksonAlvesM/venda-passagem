using SellBusTicket.Application.DTOs.Place;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Application.Interfaces
{
    public interface IGetPlacesUseCase
    {
        Task<IEnumerable<PlaceResponseDto>> ExecuteAsync();
    }
}
