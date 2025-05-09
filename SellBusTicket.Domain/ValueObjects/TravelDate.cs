using SellBusTicket.Domain.Common;
using SellBusTicket.Domain.Notification;
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
        private readonly NotificationContext _notificationContext;

        public TravelDate(DateOnly value, NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
            if (value < DateOnly.FromDateTime(DateTime.Now.Date))
            {
                _notificationContext.AddNotification("Data de viagem não pode ser no passado.", nameof(TravelDate));
                return;
            }

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
