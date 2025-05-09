using SellBusTicket.Domain.Common;

namespace SellBusTicket.Domain.ValueObjects
{
    public class PlaceName : ValueObject
    {
        public string Value { get; private set; }

        public PlaceName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Nome do local é obrigatório.");

            Value = value.Trim();
        }

        public PlaceName() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower();
        }

        public override string ToString() => Value;
    }
}
