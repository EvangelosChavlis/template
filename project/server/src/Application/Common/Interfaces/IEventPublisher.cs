namespace server.src.Application.Common.Interfaces;

/// <summary>
/// Interface for publishing events.
/// </summary>
public interface IEventPublisher
{
    Task Publish<TNotification>(TNotification notification, CancellationToken token = default) 
        where TNotification : INotification;
}
