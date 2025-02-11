namespace server.src.Application.Common.Interfaces;

/// <summary>
/// Handles notifications of type <typeparamref name="TNotification"/>.
/// </summary>
/// <typeparam name="TNotification">The type of the notification.</typeparam>
public interface INotificationHandler<TNotification> where TNotification : INotification
{
    Task Handle(TNotification notification, CancellationToken token = default);
}
