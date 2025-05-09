using SellBusTicket.Domain.Common;
using SellBusTicket.Domain.Notification;
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
        private readonly NotificationContext _notificationContext;


        public ArrivalDateTime(DateTime value, NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;

            // Verifica se a data está no passado e adiciona uma notificação
            if (value < DateTime.UtcNow)
            {
                _notificationContext.AddNotification("Data e hora de chegada não podem estar no passado.", nameof(ArrivalDateTime));
            }

            // Caso contrário, define o valor
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
