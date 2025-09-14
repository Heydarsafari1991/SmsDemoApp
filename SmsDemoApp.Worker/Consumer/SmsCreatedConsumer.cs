using MassTransit;
using SmsDemoApp.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Worker.Consumer;

public class SmsCreatedConsumer : IConsumer<SmsCreatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<SmsCreatedIntegrationEvent> context)
    {
        var message = context.Message;

        Console.WriteLine($"[Consumer] SMS Created for CustomerId: {message.CustomerId}");
        Console.WriteLine($"Text: {message.Text}");
        Console.WriteLine($"Receiver: {message.ReciverPhoneNumber}");

        await Task.CompletedTask;
    }
}
