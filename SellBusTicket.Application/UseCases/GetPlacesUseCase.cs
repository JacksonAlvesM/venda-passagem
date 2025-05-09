using SellBusTicket.Application.DTOs.Place;
using SellBusTicket.Application.Interfaces;
using SellBusTicket.Domain.Interfaces.Repositories;

namespace SellBusTicket.Application.UseCases
{
    public class GetPlacesUseCase(IPlaceRepository placeRepository) :  IGetPlacesUseCase
    {
        public async Task<IEnumerable<PlaceResponseDto>> ExecuteAsync()
        {
            var places = await placeRepository.GetAllAsync();
            return places.Select(p => new PlaceResponseDto(p.Id, p.Name.Value));
        }
    }
}
