using MassTransit;
using SmsDemoApp.Application.Messaging;

namespace SmsDemoApp.Infrastructure.CrossCutting.Messaging;

public class MassTransitEventPublisher : IEventPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public MassTransitEventPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default)
    {
        return _publishEndpoint.Publish(@event, cancellationToken);
    }
}

