// package
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Common.Interfaces;

namespace server.src.Application.Common.Events;

public class EventPublisher : IEventPublisher
{
    private readonly IServiceProvider _serviceProvider;

    public EventPublisher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Publish<TNotification>(TNotification notification, CancellationToken token = default) 
        where TNotification : INotification
    {
        using var scope = _serviceProvider.CreateScope();
        var handlers = scope.ServiceProvider.GetServices<INotificationHandler<TNotification>>().ToList();

        foreach (var handler in handlers)
        {
            await handler.Handle(notification, token);
        }
    }
}
