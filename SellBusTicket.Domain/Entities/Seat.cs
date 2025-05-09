using SellBusTicket.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.Entities
{
    public class Seat
    {
        public Guid Id { get; private set; }
        public Guid RouteId { get; private set; }
        public SeatNumber Number { get; private set; }
        public SeatAvailability Available { get; private set; }

        public Seat(Guid id, Guid routeId, SeatNumber number, SeatAvailability available)
        {
            Id = id;
            RouteId = routeId;
            Number = number;
            Available = available;
        }

        public Seat() { }

        public void Reserve()
        {
            if (!Available.Value)
                throw new InvalidOperationException("Assento já reservado.");

            Available = new SeatAvailability(false);
        }
    }
}
