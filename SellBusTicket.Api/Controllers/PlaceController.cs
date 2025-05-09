using Microsoft.AspNetCore.Mvc;
using SellBusTicket.Application.Interfaces;
using SellBusTicket.Domain.Notification;

namespace SellBusTicket.Api.Controllers;

public class PlaceController : Controller
{

    [HttpGet("places")]
    public async Task<IActionResult> Get(
        [FromServices] IGetPlacesUseCase IGetPlacesUseCase, 
        [FromServices] NotificationContext notificationContext)
    {
        var result = await IGetPlacesUseCase.ExecuteAsync();
        if (notificationContext.HasNotifications)
        {
            return BadRequest(new { errors = notificationContext.GetNotifications() });
        }
        return Ok(result);
    }
}
