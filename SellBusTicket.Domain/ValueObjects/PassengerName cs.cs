using SellBusTicket.Domain.Common;
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

        public PassengerName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome do passageiro é obrigatório.");

            if (name.Length < 2)
                throw new ArgumentException("Nome do passageiro é muito curto.");

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
