using Mediator;
using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Events;
using SmsDemoApp.Application.Features.Category.Commands;
using SmsDemoApp.Application.Messaging;
using SmsDemoApp.Application.Repositories.Common;
using SmsDemoApp.Domain.Entities.Ad;
using SmsDemoApp.Domain.Entities.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Application.Features.SMS.Command;


public class CreatePreSMSCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher) : IRequestHandler<CreatePreSMSCommand, OperationResult<bool>>
{
    public async ValueTask<OperationResult<bool>> Handle(CreatePreSMSCommand request, CancellationToken cancellationToken)
    {
        var sms = new SMSEntity()
        {
            CreatedDate = DateTime.Now,
            CustomerId = request.CustomerId,
            ReciverPhoneNumber = request.ReciverPhoneNumber,
            Status = MessageDelivaryStatus.Pending,
            Text = request.Text,
            Id = Guid.NewGuid()
        };

        await unitOfWork.SMSRepository.CreateAsync(sms, cancellationToken);

        var smsCreatedEvent = new SmsCreatedIntegrationEvent(
             CustomerId: sms.CustomerId,
             Text: sms.Text,
             ReciverPhoneNumber: sms.ReciverPhoneNumber,
             Status: sms.Status,
             Id: sms.Id,
             EventId: Guid.NewGuid()
         );

       
        await eventPublisher.PublishAsync(smsCreatedEvent, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        return OperationResult<bool>.SuccessResult(true);
    }
}
