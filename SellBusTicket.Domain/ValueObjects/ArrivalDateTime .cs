using SellBusTicket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.ValueObjects
{
    public class ArrivalDateTime : ValueObject
    {
        public DateTime Value { get; private set; }

        public ArrivalDateTime(DateTime value)
        {
            if (value < DateTime.Now)
                throw new ArgumentException("Data e hora de chegada não podem estar no passado.");

            Value = value;
        }

        private ArrivalDateTime() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString("yyyy-MM-ddTHH:mm:ss");

    }
}
