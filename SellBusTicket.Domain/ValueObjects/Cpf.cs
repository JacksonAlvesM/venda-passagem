using SellBusTicket.Domain.Common;
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

        public Cpf(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("CPF não pode ser vazio.");

            var digitsOnly = Regex.Replace(value, "[^0-9]", "");

            if (digitsOnly.Length != 11 || !digitsOnly.All(char.IsDigit))
                throw new ArgumentException("CPF inválido.");

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
