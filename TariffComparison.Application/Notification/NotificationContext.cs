using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TariffComparison.Application.Notification
{
    public class NotificationContext : INotificationContext
    {
        private readonly List<Notification> _notifications;

        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public bool HasErrorNotifications
            => _notifications.Any(x => x.Type == NotificationType.Error);

        public void NotifySuccess(string message)
            => Notify(message, NotificationType.Success);

        public void NotifyError(string message)
            => Notify(message, NotificationType.Error);

        private void Notify(string message, NotificationType type)
            => _notifications.Add(new Notification(type, message));

        public IEnumerable<Notification> GetErrorNotifications()
            => _notifications.Where(x => x.Type == NotificationType.Error);
    }
}
