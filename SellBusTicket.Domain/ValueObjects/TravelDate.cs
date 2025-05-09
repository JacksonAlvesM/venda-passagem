using SellBusTicket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.ValueObjects
{
    public class TravelDate : ValueObject
    {
        public DateOnly Value { get; private set; }

        public TravelDate(DateOnly value)
        {
            if (value < DateOnly.FromDateTime(DateTime.Now.Date))
                throw new ArgumentException("Data de viagem não pode ser no passado.");

            Value = value;
        }

        private TravelDate() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString("dd/MM/yyyy");
    }
}
