using System.Collections.Generic;

namespace TariffComparison.Application.Notification
{
    public interface INotificationContext
    {
        bool HasErrorNotifications { get; }
        void NotifyError(string message);
        void NotifySuccess(string message);
        IEnumerable<Notification> GetErrorNotifications();
    }
}