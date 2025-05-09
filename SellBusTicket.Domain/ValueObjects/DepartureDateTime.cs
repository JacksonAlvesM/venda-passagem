using SellBusTicket.Domain.Common;
using SellBusTicket.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.ValueObjects
{
    public class DepartureDateTime : ValueObject
    {
        public DateTime Value { get; private set; }
        private readonly NotificationContext _notificationContext;

        public DepartureDateTime(DateTime value, NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;

            if (value < DateTime.Now)
            {
                notificationContext.AddNotification("Data e hora de partida não podem estar no passado.", nameof(DepartureDateTime));
                return;
            }

            Value = value;
        }

        private DepartureDateTime() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}
