// packages
using Microsoft.Extensions.DependencyInjection;

// source
using server.src.Application.Common.Interfaces;

namespace server.src.Application.Common.Services;

public class RequestExecutor
{
    private readonly IServiceProvider _serviceProvider;

    public RequestExecutor(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Execute<TRequest, TResponse>(TRequest request, CancellationToken token = default)
        where TRequest : class
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request), "Request cannot be null");

        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetRequiredService(handlerType);

        var handleMethod = handlerType.GetMethod("Handle") ??
            throw new InvalidOperationException($"Handler method 'Handle' not found for {handlerType.FullName}");

        var result = handleMethod.Invoke(handler, [request, token]) as Task<TResponse> ??
            throw new InvalidOperationException("Handler returned a null response.");

        return await result;
    }
}