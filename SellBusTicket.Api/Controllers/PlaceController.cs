using Microsoft.AspNetCore.Mvc;
using SellBusTicket.Application.Interfaces;

namespace SellBusTicket.Api.Controllers;

public class PlaceController : Controller
{

    [HttpGet("places")]
    public async Task<IActionResult> Get(
        [FromServices] IGetPlacesUseCase IGetPlacesUseCase)
    {
        var result = await IGetPlacesUseCase.ExecuteAsync();
        return Ok(result);
    }
}
