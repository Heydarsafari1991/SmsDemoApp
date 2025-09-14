using Mediator;
using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Messaging;
using SmsDemoApp.Application.Repositories.Common;
using SmsDemoApp.Domain.Entities.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsDemoApp.Application.Features.SMS.Command;


public class ProcessPreSMSCommandHandler(IUnitOfWork unitOfWork, IEventPublisher eventPublisher) : IRequestHandler<ProcessPreSMSCommand, OperationResult<bool>>
{
    public async ValueTask<OperationResult<bool>> Handle(ProcessPreSMSCommand request, CancellationToken cancellationToken)
    {
        var sms = await unitOfWork.SMSRepository.GetByIdAysnc(request.Id, cancellationToken);

        var haveMoney = await unitOfWork.CustomerBalanceRepository.
        GetAffectedRowsOfUpdatingCustomerBalance(
            request.CustomerId,
            200,
            cancellationToken

            );
        if (haveMoney)
        {
            // call sms service 

            sms.Status = MessageDelivaryStatus.Success;
            
        }
        else
        {
            sms.Status = MessageDelivaryStatus.NoMoney;
        }
        await unitOfWork.CommitAsync(cancellationToken);

        return OperationResult<bool>.SuccessResult(true);
    }
}
