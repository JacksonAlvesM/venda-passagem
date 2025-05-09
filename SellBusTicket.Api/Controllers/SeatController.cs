using Microsoft.AspNetCore.Mvc;
using SellBusTicket.Application.DTOs.Seat;
using SellBusTicket.Application.Interfaces;
using SellBusTicket.Domain.Interfaces.Repositories;
using SellBusTicket.Domain.Notification;

namespace SellBusTicket.Api.Controllers;

public class SeatController() : Controller
{

    [HttpGet("{routeId}")]
    public async Task<IActionResult> GetSeatsByRoute(
        [FromServices] IGetSeatsByRouteUseCase getSeatsByRouteUseCase,
        [FromServices] NotificationContext notificationContext,
        Guid routeId)
    {
        var result = await getSeatsByRouteUseCase.ExecuteAsync(routeId);
        if (notificationContext.HasNotifications)
        {
            return BadRequest(new { errors = notificationContext.GetNotifications() });
        }

        return Ok(result);
    }
}
