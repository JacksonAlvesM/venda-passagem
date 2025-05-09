using SellBusTicket.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.Entities
{
    public class Route
    {
        public Guid Id { get; private set; }
        public Guid OriginId { get; private set; }
        public Guid DestinationId { get; private set; }
        public DepartureDateTime Departure { get; private set; }
        public ArrivalDateTime Arrival { get; private set; }

        public Route(Guid id, Guid originId, Guid destinationId, DepartureDateTime departure, ArrivalDateTime arrival)
        {
            Id = id;
            OriginId = originId;
            DestinationId = destinationId;
            Departure = departure;
            Arrival = arrival;
        }
        public Route() { }
    }
}

