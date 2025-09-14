using Mediator;
using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Events;
using SmsDemoApp.Application.Features.Ad.Commands;
using SmsDemoApp.Application.Messaging;
using SmsDemoApp.Application.Repositories.Common;
using SmsDemoApp.Domain.Entities.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Application.Features.SMS.Command;

public class CreateSMSCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher) : IRequestHandler<CreateSMSCommand, OperationResult<bool>>
{
    public async ValueTask<OperationResult<bool>> Handle(CreateSMSCommand request, CancellationToken cancellationToken)
    {

        var haveMoney = await unitOfWork.CustomerBalanceRepository.
        GetAffectedRowsOfUpdatingCustomerBalance(
            request.CustomerId,
            200,
            cancellationToken

            );
        if (haveMoney)
        {
            // call sms service 

            var sms = new SMSEntity()
            {
                CreatedDate = DateTime.Now,
                CustomerId = request.CustomerId,
                ReciverPhoneNumber = request.ReciverPhoneNumber,
                Status = MessageDelivaryStatus.Success,
                Text = request.Text
            };
            
            await unitOfWork.SMSRepository.CreateAsync(sms, cancellationToken);


            await unitOfWork.CommitAsync(cancellationToken);
        }
        else
        {
            return OperationResult<bool>.DomainFailureResult("thre is No Money Left !!! ");
        }

            return OperationResult<bool>.SuccessResult(true);
    }
}