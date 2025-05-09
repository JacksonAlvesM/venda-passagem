using SellBusTicket.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.Entities
{
    public class Place
    {
        public Guid Id { get; private set; }
        public PlaceName Name { get; private set; }

        public Place(Guid id, PlaceName name)
        {
            Id = id;
            Name = name;
        }

        private Place() { }
    }
}
