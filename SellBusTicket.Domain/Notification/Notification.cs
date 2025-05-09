using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.Notification
{
    public class Notification
    {
        public string Key { get; }
        public string Message { get; }

        public Notification(string message, string key = "")
        {
            Key = key;
            Message = message;
        }
    }
}
