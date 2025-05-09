using Moq;
using Xunit;
using SellBusTicket.Application.UseCases;
using SellBusTicket.Application.DTOs.Seat;
using SellBusTicket.Domain.Interfaces.Repositories;
using SellBusTicket.Domain.Notification;
using SellBusTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SellBusTicket.Domain.ValueObjects;

namespace SellBusTicket.Application.Tests
{
    public class GetSeatsByRouteUseCaseTests
    {
        private readonly Mock<ISeatRepository> _seatRepositoryMock;
        private readonly NotificationContext _notificationContext;
        private readonly GetSeatsByRouteUseCase _useCase;

        public GetSeatsByRouteUseCaseTests()
        {
            _seatRepositoryMock = new Mock<ISeatRepository>();
            _notificationContext = new NotificationContext();
            _useCase = new GetSeatsByRouteUseCase(_seatRepositoryMock.Object, _notificationContext);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmptyList_WhenNoSeatsFound()
        {
            var routeId = Guid.NewGuid();
            _seatRepositoryMock.Setup(repo => repo.GetSeatsByRouteIdAsync(routeId))
                .ReturnsAsync(new List<Seat>());

            var result = await _useCase.ExecuteAsync(routeId);

            Assert.Empty(result);
            Assert.Single(_notificationContext.GetNotifications()); 
            Assert.Equal("Nenhum assento encontrado para a rota especificada.", _notificationContext.GetNotifications().First());
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnSeats_WhenSeatsFound()
        {
            var routeId = Guid.NewGuid();
            var notificationContext = new NotificationContext();  

            var seats = new List<Seat>
            {
                new Seat(Guid.NewGuid(), routeId, new SeatNumber(2, notificationContext), new SeatAvailability(true)),
                new Seat(Guid.NewGuid(), routeId, new SeatNumber(3, notificationContext), new SeatAvailability(false))
            };

            _seatRepositoryMock.Setup(repo => repo.GetSeatsByRouteIdAsync(routeId))
                .ReturnsAsync(seats);

            var result = await _useCase.ExecuteAsync(routeId);

            Assert.NotEmpty(result); 
            Assert.Equal(2, result.Count());
            Assert.Contains(result, s => s.Number == 2 && s.Available); 
            Assert.Contains(result, s => s.Number == 3 && !s.Available); 
        }

        [Fact]
        public async Task ExecuteAsync_ShouldAddNotification_WhenNoSeatsFound()
        {
            var routeId = Guid.NewGuid();
            _seatRepositoryMock.Setup(repo => repo.GetSeatsByRouteIdAsync(routeId))
                .ReturnsAsync(new List<Seat>());

            await _useCase.ExecuteAsync(routeId);

            Assert.Single(_notificationContext.GetNotifications());
            Assert.Equal("Nenhum assento encontrado para a rota especificada.", _notificationContext.GetNotifications().First());
        }
    }
}
