using SellBusTicket.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.Entities
{
    public class Trip
    {
        public Guid Id { get; private set; }
        public Guid RouteId { get; private set; }
        public PassengerName Name { get; private set; }
        public Cpf Cpf { get; private set; }
        public SeatNumber Seat { get; private set; }

        public Trip(Guid id, Guid routeId, PassengerName name, Cpf cpf, SeatNumber seat)
        {
            Id = id;
            RouteId = routeId;
            Name = name;
            Cpf = cpf;
            Seat = seat;
        }

        public Trip() { }
    }

}
