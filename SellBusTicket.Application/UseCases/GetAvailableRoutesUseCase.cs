using SellBusTicket.Application.DTOs.Route;
using SellBusTicket.Application.Interfaces;
using SellBusTicket.Domain.Interfaces.Repositories;
using SellBusTicket.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Application.UseCases
{
    public class GetAvailableRoutesUseCase(
        IRouteRepository routeRepository,
        IPlaceRepository placeRepository,
        NotificationContext notificationContext) : IGetAvailableRoutesUseCase
    {
        public async Task<IEnumerable<RouteResponseDto>> ExecuteAsync(RouteFilterRequestDto request)
        {
            if (request.Date == default)
            {
                notificationContext.AddNotification("A data fornecida não é válida.", nameof(RouteFilterRequestDto.Date));
                return Enumerable.Empty<RouteResponseDto>();
            }

            if (request.OriginId == Guid.Empty)
            {
                notificationContext.AddNotification("A origem não foi especificada.", nameof(RouteFilterRequestDto.OriginId));
                return Enumerable.Empty<RouteResponseDto>();
            }

            if (request.DestinationId == Guid.Empty)
            {
                notificationContext.AddNotification("O destino não foi especificado.", nameof(RouteFilterRequestDto.DestinationId));
                return Enumerable.Empty<RouteResponseDto>();
            }

            var routes = await routeRepository.GetRoutesByFilterAsync(request.Date, request.OriginId, request.DestinationId);

            var origin = await placeRepository.GetByIdAsync(request.OriginId);
            var destination = await placeRepository.GetByIdAsync(request.DestinationId);

            if (origin == null)
            {
                notificationContext.AddNotification("Origem não encontrada.", nameof(RouteFilterRequestDto.OriginId));
            }

            if (destination == null)
            {
                notificationContext.AddNotification("Destino não encontrado.", nameof(RouteFilterRequestDto.DestinationId));
            }

            if (routes == null || !routes.Any())
            {
                notificationContext.AddNotification("Nenhuma rota encontrada para o intervalo de datas especificado.", nameof(RouteFilterRequestDto.Date));
            }

            if (notificationContext.HasNotifications)
            {
                return Enumerable.Empty<RouteResponseDto>();
            }

            return routes.Select(r => new RouteResponseDto(
                r.Id,
                r.Departure.Value,
                r.Arrival.Value,
                origin.Name.Value,
                destination.Name.Value
            ));
        }
    }
}
