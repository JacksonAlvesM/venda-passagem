using Moq;
using SellBusTicket.Application.DTOs.Trip;
using SellBusTicket.Application.Interfaces;
using SellBusTicket.Application.UseCases;
using SellBusTicket.Domain.Entities;
using SellBusTicket.Domain.Interfaces.Repositories;
using SellBusTicket.Domain.Notification;
using SellBusTicket.Domain.ValueObjects;

namespace SellBusTicket.Tests
{
    public class SellTicketUseCaseTests
    {
        private readonly Mock<IRouteRepository> _routeRepositoryMock;
        private readonly Mock<ISeatRepository> _seatRepositoryMock;
        private readonly Mock<ITripRepository> _tripRepositoryMock;
        private readonly NotificationContext _notificationContext;
        private readonly ISellTicketUseCase _useCase;

        public SellTicketUseCaseTests()
        {
            _routeRepositoryMock = new Mock<IRouteRepository>();
            _seatRepositoryMock = new Mock<ISeatRepository>();
            _tripRepositoryMock = new Mock<ITripRepository>();
            _notificationContext = new NotificationContext();
            _useCase = new SellTicketUseCase(_routeRepositoryMock.Object, _seatRepositoryMock.Object, _tripRepositoryMock.Object, _notificationContext);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldAddNotification_WhenRouteNotFound()
        {
            var request = new TripRequestDto
            {
                RouteId = Guid.NewGuid(),
                Seat = 1,
                Cpf = "12345678901", 
                Name = "John Doe"
            };

            _routeRepositoryMock.Setup(repo => repo.GetByIdAsync(request.RouteId))
                .ReturnsAsync((Route)null);  

            var result = await _useCase.ExecuteAsync(request);

            Assert.Null(result);
            Assert.True(_notificationContext.HasNotifications); 
            Assert.Contains("Rota não encontrada.", _notificationContext.GetNotifications());
        }

        [Fact]
        public async Task ExecuteAsync_ShouldAddNotification_WhenSeatNotAvailable()
        {
            // Arrange
            var routeId = Guid.NewGuid();
            var request = new TripRequestDto
            {
                RouteId = routeId,
                Seat = 1,
                Cpf = "12345678901",
                Name = "John Doe"
            };

            var route = new Route(
                request.RouteId,
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DepartureDateTime(DateTime.UtcNow.AddHours(1), _notificationContext), 
                new ArrivalDateTime(DateTime.UtcNow.AddHours(5), _notificationContext)   
            );
            var seat = new Seat(Guid.NewGuid(), routeId, new SeatNumber(1, _notificationContext), new SeatAvailability(false));  

            _routeRepositoryMock.Setup(repo => repo.GetByIdAsync(routeId)).ReturnsAsync(route);
            _seatRepositoryMock.Setup(repo => repo.GetSeatByRouteAndNumberAsync(routeId, 1)).ReturnsAsync(seat);

            // Act
            var result = await _useCase.ExecuteAsync(request);

            // Assert
            Assert.Null(result); 
            Assert.True(_notificationContext.HasNotifications); 
            Assert.Contains("Assento não disponível.", _notificationContext.GetNotifications());
        }

        // Teste 3: Quando o CPF é inválido
        [Fact]
        public async Task ExecuteAsync_ShouldAddNotification_WhenCpfIsInvalid()
        {
            // Arrange
            var request = new TripRequestDto
            {
                RouteId = Guid.NewGuid(),
                Seat = 1,
                Cpf = "12345",  
                Name = "John Doe"
            };

            var route = new Route(
                request.RouteId,
                Guid.NewGuid(), 
                Guid.NewGuid(),
                new DepartureDateTime(DateTime.UtcNow.AddHours(1), _notificationContext),
                new ArrivalDateTime(DateTime.UtcNow.AddHours(5), _notificationContext)  
            );
            var seat = new Seat(Guid.NewGuid(), request.RouteId, new SeatNumber(1, _notificationContext), new SeatAvailability(true));  

            _routeRepositoryMock.Setup(repo => repo.GetByIdAsync(request.RouteId)).ReturnsAsync(route);
            _seatRepositoryMock.Setup(repo => repo.GetSeatByRouteAndNumberAsync(request.RouteId, 1)).ReturnsAsync(seat);

            var result = await _useCase.ExecuteAsync(request);

            Assert.Null(result); 
            Assert.True(_notificationContext.HasNotifications);
            Assert.Contains("CPF inválido.", _notificationContext.GetNotifications());
        }

        [Fact]
        public async Task ExecuteAsync_ShouldCreateTrip_WhenDataIsValid()
        {
            // Arrange
            var request = new TripRequestDto
            {
                RouteId = Guid.NewGuid(),
                Seat = 1,
                Cpf = "12345678901", 
                Name = "John Doe"
            };

            var route = new Route(
                request.RouteId,
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DepartureDateTime(DateTime.UtcNow.AddHours(1), _notificationContext),
                new ArrivalDateTime(DateTime.UtcNow.AddHours(5), _notificationContext)
            );

            var seat = new Seat(
                Guid.NewGuid(),
                request.RouteId,
                new SeatNumber(1, _notificationContext),
                new SeatAvailability(true)
            );

            _routeRepositoryMock.Setup(repo => repo.GetByIdAsync(request.RouteId)).ReturnsAsync(route);
            _seatRepositoryMock.Setup(repo => repo.GetSeatByRouteAndNumberAsync(request.RouteId, 1)).ReturnsAsync(seat);
            _tripRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Trip>())).Returns(Task.CompletedTask);

            var result = await _useCase.ExecuteAsync(request);

            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            _tripRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Trip>()), Times.Once);
        }
    }
}
