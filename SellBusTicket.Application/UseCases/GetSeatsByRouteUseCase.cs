using SellBusTicket.Application.DTOs.Seat;
using SellBusTicket.Application.Interfaces;
using SellBusTicket.Domain.Interfaces.Repositories;
using SellBusTicket.Domain.Notification;

namespace SellBusTicket.Application.UseCases
{
    public class GetSeatsByRouteUseCase(ISeatRepository seatRepository, NotificationContext notificationContext) : IGetSeatsByRouteUseCase
    {

        public async Task<IEnumerable<SeatResponseDto>> ExecuteAsync(Guid routeId)
        {
            var seats = await seatRepository.GetSeatsByRouteIdAsync(routeId);

            if (seats == null || !seats.Any())
            {
                notificationContext.AddNotification("Nenhum assento encontrado para a rota especificada.", nameof(routeId));
                return Enumerable.Empty<SeatResponseDto>();
            }

            return seats.Select(s => new SeatResponseDto(s.Number.Value, s.Available.Value));
        }
    }
}
