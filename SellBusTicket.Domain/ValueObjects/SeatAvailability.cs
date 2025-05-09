using SellBusTicket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.ValueObjects
{
    public class SeatAvailability : ValueObject
    {
        public bool Value { get; private set; }

        public SeatAvailability(bool value)
        {
            Value = value;
        }

        private SeatAvailability() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value ? "Disponível" : "Ocupado";
    }

}
