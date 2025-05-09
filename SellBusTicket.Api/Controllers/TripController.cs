using Microsoft.AspNetCore.Mvc;
using SellBusTicket.Application.DTOs.Trip;
using SellBusTicket.Application.Interfaces;
using SellBusTicket.Domain.Notification;

namespace SellBusTicket.Api.Controllers;

[Route("trip")]
public class TripController : Controller
{

    [HttpPost]
    public async Task<IActionResult> Post(
        [FromServices] ISellTicketUseCase sellTicketUseCase,
        [FromServices] NotificationContext notificationContext,
        [FromBody] TripRequestDto request)
    {
        var result = await sellTicketUseCase.ExecuteAsync(request);
        if (notificationContext.HasNotifications)
        {
            return BadRequest(new { errors = notificationContext.GetNotifications() });
        }

        return Ok(result);
    }
}
