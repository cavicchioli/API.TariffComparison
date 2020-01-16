using MediatR;

namespace TariffComparison.Application.Notification
{
    public class Notification : INotification
    {
        public NotificationType Type { get; protected set; }
        public string Value { get; protected set; }

        public Notification(NotificationType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
