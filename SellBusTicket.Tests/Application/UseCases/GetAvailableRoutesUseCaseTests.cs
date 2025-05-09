using Moq;
using SellBusTicket.Application.DTOs.Route;
using SellBusTicket.Application.UseCases;
using SellBusTicket.Domain.Interfaces.Repositories;
using SellBusTicket.Domain.Notification;
using SellBusTicket.Domain.Entities;
using SellBusTicket.Domain.ValueObjects;

namespace SellBusTicket.Tests.Application.UseCases;
public class GetAvailableRoutesUseCaseTests
{
    private readonly Mock<IRouteRepository> _routeRepositoryMock;
    private readonly Mock<IPlaceRepository> _placeRepositoryMock;
    private readonly NotificationContext _notificationContext;
    private readonly GetAvailableRoutesUseCase _useCase;

    public GetAvailableRoutesUseCaseTests()
    {
        _routeRepositoryMock = new Mock<IRouteRepository>();
        _placeRepositoryMock = new Mock<IPlaceRepository>();
        _notificationContext = new NotificationContext();
        _useCase = new GetAvailableRoutesUseCase(
            _routeRepositoryMock.Object,
            _placeRepositoryMock.Object,
            _notificationContext
        );
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmpty_WhenDateIsInvalid()
    {
        var request = new RouteFilterRequestDto
        {
            Date = default,
            OriginId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid()
        };

        var result = await _useCase.ExecuteAsync(request);

        Assert.Empty(result);
        Assert.Single(_notificationContext.GetNotifications());
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmpty_WhenOriginIdIsEmpty()
    {
        var request = new RouteFilterRequestDto
        {
            Date = DateTime.Now,
            OriginId = Guid.Empty,
            DestinationId = Guid.NewGuid()
        };

        var result = await _useCase.ExecuteAsync(request);

        Assert.Empty(result);
        Assert.Single(_notificationContext.GetNotifications());
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmpty_WhenDestinationIdIsEmpty()
    {
        var request = new RouteFilterRequestDto
        {
            Date = DateTime.Now,
            OriginId = Guid.NewGuid(),
            DestinationId = Guid.Empty
        };

        var result = await _useCase.ExecuteAsync(request);

        Assert.Empty(result);
        Assert.Single(_notificationContext.GetNotifications());
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmpty_WhenNoRoutesFound()
    {
        var request = new RouteFilterRequestDto
        {
            Date = DateTime.Now,
            OriginId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid()
        };

        var origin = new Place(Guid.NewGuid(), new PlaceName("Origin Place"));
        var destination = new Place(Guid.NewGuid(), new PlaceName("Destination Place"));

        _routeRepositoryMock.Setup(r => r.GetRoutesByFilterAsync(It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new List<Route>());
        _placeRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(origin);

        var result = await _useCase.ExecuteAsync(request);

        Assert.Empty(result);
        Assert.Single(_notificationContext.GetNotifications());
    }

    [Fact]
    public async Task ExecuteAsync_ShouldReturnEmpty_WhenPlaceNotFound()
    {
        var request = new RouteFilterRequestDto
        {
            Date = DateTime.Now,
            OriginId = Guid.NewGuid(),
            DestinationId = Guid.NewGuid()
        };

        Place? origin = null;
        Place? destination = null;

        _routeRepositoryMock.Setup(r => r.GetRoutesByFilterAsync(It.IsAny<DateTime>(), It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ReturnsAsync(new List<Route>());
        _placeRepositoryMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => id == request.OriginId ? origin : destination);

        var result = await _useCase.ExecuteAsync(request);

        Assert.Empty(result);
        Assert.Equal(3, _notificationContext.GetNotifications().Count());
    }
}
