using SellBusTicket.Domain.Common;
using SellBusTicket.Domain.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.ValueObjects
{
    public class Cpf : ValueObject
    {
        public string Value { get; private set; }
        private readonly NotificationContext _notificationContext;

        public Cpf(string value, NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;

            if (string.IsNullOrWhiteSpace(value))
            {
                _notificationContext.AddNotification("CPF não pode ser vazio.", nameof(Cpf));
            }

            var digitsOnly = Regex.Replace(value, "[^0-9]", "");

            if (digitsOnly.Length != 11 || !digitsOnly.All(char.IsDigit))
            {
                _notificationContext.AddNotification("CPF inválido.", nameof(Cpf));
                return; 
            }

            Value = digitsOnly;
        }

        private Cpf() { }

        public override string ToString()
        {
            return Convert.ToUInt64(Value).ToString(@"000\.000\.000\-00");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
