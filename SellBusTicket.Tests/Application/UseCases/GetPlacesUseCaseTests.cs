using Moq;
using Xunit;
using SellBusTicket.Application.UseCases;
using SellBusTicket.Application.DTOs.Place;
using SellBusTicket.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SellBusTicket.Domain.Entities;
using SellBusTicket.Domain.ValueObjects;

namespace SellBusTicket.Tests.Application.UseCases;

public class GetPlacesUseCaseTests
{
    private readonly Mock<IPlaceRepository> _placeRepositoryMock;
    private readonly GetPlacesUseCase _useCase;

    public GetPlacesUseCaseTests()
    {
        _placeRepositoryMock = new Mock<IPlaceRepository>();
        _useCase = new GetPlacesUseCase(_placeRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmptyList_WhenNoPlacesExist()
    {
        _placeRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(new List<Place>());

        var result = await _useCase.ExecuteAsync();

        Assert.Empty(result);
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnPlaceDtos_WhenPlacesExist()
    {
        var places = new List<Place>
        {
            new Place(Guid.NewGuid(), new PlaceName("Place 1")),
            new Place(Guid.NewGuid(), new PlaceName("Place 2"))
        };

        _placeRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(places);

        var result = await _useCase.ExecuteAsync();

        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Name == "Place 1");
        Assert.Contains(result, p => p.Name == "Place 2");
    }

    [Fact]
    public async Task ExecuteAsync_ShouldHandleRepositoryFailure()
    {
        _placeRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ThrowsAsync(new Exception("Repository error"));

        await Assert.ThrowsAsync<Exception>(() => _useCase.ExecuteAsync());
    }
}
