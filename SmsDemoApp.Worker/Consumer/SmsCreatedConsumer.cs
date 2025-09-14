using MassTransit;
using Mediator;
using SmsDemoApp.Application.Events;
using SmsDemoApp.Application.Features.SMS.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Worker.Consumer;

public class SmsCreatedConsumer(ISender sender) : IConsumer<SmsCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<SmsCreatedIntegrationEvent> context)
    {
        var command = new ProcessPreSMSCommand(
            context.Message.Id,
            context.Message.CustomerId,
            context.Message.Text,
            context.Message.ReciverPhoneNumber
        );
        await sender.Send(command);

        await Task.CompletedTask;
    }
}
