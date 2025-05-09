using SellBusTicket.Domain.Common;

namespace SellBusTicket.Domain.ValueObjects
{
    public class SeatNumber : ValueObject
    {
        public int Value { get; private set; }

        public SeatNumber(int value)
        {
            if (value <= 0)
                throw new ArgumentException("Número de poltrona deve ser positivo.");

            Value = value;
        }

        private SeatNumber() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
