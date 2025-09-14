using SmsDemoApp.Domain.Entities.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Application.Events;

public record SmsCreatedIntegrationEvent(
    int CustomerId,
    string Text,
    string ReciverPhoneNumber,
    MessageDelivaryStatus Status,
    Guid Id,
    Guid EventId
);

