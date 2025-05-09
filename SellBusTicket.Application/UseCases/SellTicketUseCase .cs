using SellBusTicket.Application.DTOs.Trip;
using SellBusTicket.Application.Interfaces;
using SellBusTicket.Domain.Entities;
using SellBusTicket.Domain.Interfaces.Repositories;
using SellBusTicket.Domain.ValueObjects;
using SellBusTicket.Domain.Notification;

namespace SellBusTicket.Application.UseCases
{
    public class SellTicketUseCase(
        IRouteRepository routeRepository,
        ISeatRepository seatRepository,
        ITripRepository tripRepository,
        NotificationContext notificationContext) : ISellTicketUseCase
    {

        public async Task<TripResponseDto> ExecuteAsync(TripRequestDto request)
        {
            // Validar a rota
            var route = await routeRepository.GetByIdAsync(request.RouteId);
            if (route == null)
            {
                notificationContext.AddNotification("Rota não encontrada.", nameof(TripRequestDto.RouteId));
                return null;
            }

            var seat = await seatRepository.GetSeatByRouteAndNumberAsync(request.RouteId, request.Seat);
            if (seat == null || !seat.Available.Value)
            {
                notificationContext.AddNotification("Assento não disponível.", nameof(TripRequestDto.Seat));
                return null; 
            }

            try
            {
                var cpf = new Cpf(request.Cpf, notificationContext); 
            }
            catch (ArgumentException ex)
            {
                notificationContext.AddNotification(ex.Message, nameof(TripRequestDto.Cpf));
                return null; 
            }

            // Criar a viagem
            var trip = new Trip(
                id: Guid.NewGuid(),
                routeId: request.RouteId,
                name: new PassengerName(request.Name, notificationContext),
                cpf: new Cpf(request.Cpf, notificationContext),
                seat: new SeatNumber(request.Seat, notificationContext)
            );

            if (notificationContext.HasNotifications)
                return null; 

            await tripRepository.AddAsync(trip);

            seat.Reserve();
            await seatRepository.SaveChangesAsync();

            if (notificationContext.HasNotifications)
                return null; 

            return new TripResponseDto(trip.Id);
        }
    }
}
