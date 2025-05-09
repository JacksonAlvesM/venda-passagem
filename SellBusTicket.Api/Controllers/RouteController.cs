using Microsoft.AspNetCore.Mvc;
using SellBusTicket.Application.DTOs.Route;
using SellBusTicket.Application.Interfaces;
using SellBusTicket.Domain.Notification;

namespace SellBusTicket.Api.Controllers;

public class RouteController : Controller
{
    [HttpPost("routes")]
    public async Task<IActionResult> GetAvailableRoutes(
            [FromServices] IGetAvailableRoutesUseCase useCase,
            [FromServices] NotificationContext _notificationContext,
            [FromBody] RouteFilterRequestDto request)
    {
        var result = await useCase.ExecuteAsync(request);

        if (_notificationContext.HasNotifications)
        {
            return BadRequest(new { errors = _notificationContext.GetNotifications() });
        }

        return Ok(result);
    }
}
