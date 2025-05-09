using SellBusTicket.Application.DTOs.Trip;

namespace SellBusTicket.Application.Interfaces
{
    public interface ISellTicketUseCase
    {
        Task<TripResponseDto?> ExecuteAsync(TripRequestDto request);
    }
}
