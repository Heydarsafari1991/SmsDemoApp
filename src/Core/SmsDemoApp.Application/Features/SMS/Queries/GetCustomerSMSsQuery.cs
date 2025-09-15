using FluentValidation;
using Mediator;
using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;
using SmsDemoApp.Domain.Entities.Message;

namespace SmsDemoApp.Application.Features.SMS.Queries;



public record GetCustomerSMSsQuery(int CustomerId, MessageDelivaryStatus? Status = null, string? Text = null, string? ReciverPhoneNumber = null, int Page = 1, int PageSize = 10)
    : IValidatableModel<GetCustomerSMSsQuery>, IRequest<OperationResult<GetCustomerSMSsQueryResult>>
{
    public IValidator<GetCustomerSMSsQuery> Validate(ValidationModelBase<GetCustomerSMSsQuery> validator)
    {
        validator.RuleFor(c => c.CustomerId)
            .NotEmpty();

        return validator;
    }
}
