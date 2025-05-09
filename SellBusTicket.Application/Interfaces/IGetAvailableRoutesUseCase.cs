using SellBusTicket.Application.DTOs.Route;

namespace SellBusTicket.Application.Interfaces
{
    public interface IGetAvailableRoutesUseCase
    {
        Task<IEnumerable<RouteResponseDto>> ExecuteAsync(RouteFilterRequestDto request);
    }
}
