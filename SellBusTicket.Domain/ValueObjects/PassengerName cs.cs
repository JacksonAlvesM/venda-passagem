using SellBusTicket.Domain.Common;
using SellBusTicket.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.ValueObjects
{
    public class PassengerName : ValueObject
    {
        public string Value { get; private set; }
        private readonly NotificationContext _notificationContext;

        public PassengerName(string name, NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
            if (string.IsNullOrWhiteSpace(name))
            {
                _notificationContext.AddNotification("Nome do passageiro é obrigatório.", nameof(PassengerName));
                return;
            }

            if (name.Length < 2)
            {
                _notificationContext.AddNotification("Nome do passageiro é muito curto.", nameof(PassengerName));
                return;
            }

            Value = name.Trim();
        }

        private PassengerName() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value.ToLower(); // Comparar ignorando caixa
        }

        public override string ToString() => Value;
    }
}
