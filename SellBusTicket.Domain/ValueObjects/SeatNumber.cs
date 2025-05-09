using SellBusTicket.Domain.Common;
using SellBusTicket.Domain.Notification;

namespace SellBusTicket.Domain.ValueObjects
{
    public class SeatNumber : ValueObject
    {
        public int Value { get; private set; }
        private readonly NotificationContext _notificationContext;

        public SeatNumber(int value, NotificationContext notificationContext)
        {
            _notificationContext = new NotificationContext();
            if (value < 0)
            {
                _notificationContext.AddNotification("Número de poltrona deve ser maior que zero.", nameof(SeatNumber));
                return;
            }

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
