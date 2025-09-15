using Mediator;
using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Repositories.Common;

namespace SmsDemoApp.Application.Features.SMS.Queries;


public class GetCustomerSMSsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCustomerSMSsQuery, OperationResult<GetCustomerSMSsQueryResult>>
{
    public async ValueTask<OperationResult<GetCustomerSMSsQueryResult>> Handle(GetCustomerSMSsQuery request, CancellationToken cancellationToken)
    {
        var (dbCustomerSMSs, totalCount) = await unitOfWork.SMSRepository.Search(
           request.CustomerId, request.Status, request.Text,
           request.ReciverPhoneNumber, request.Page, request.PageSize, cancellationToken
       );

        var CustomerSMSs = dbCustomerSMSs.Select(x => new CustomerSMS(x.Id, x.CustomerId, x.Text, x.ReciverPhoneNumber, x.Status)).ToList();


        return OperationResult<GetCustomerSMSsQueryResult>.SuccessResult(new GetCustomerSMSsQueryResult(totalCount, CustomerSMSs));
    }
}