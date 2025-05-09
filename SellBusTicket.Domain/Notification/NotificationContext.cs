using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellBusTicket.Domain.Notification
{
    public class NotificationContext
    {
        private readonly List<Notification> _notifications = new();

        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public void AddNotification(string message, string key = "") =>
            _notifications.Add(new Notification(message, key));

        public void AddNotifications(IEnumerable<Notification> notifications) =>
            _notifications.AddRange(notifications);

        public void Clear() => _notifications.Clear();
        public IEnumerable<string> GetNotifications() =>
            _notifications.Select(n => n.Message);
    }
}
