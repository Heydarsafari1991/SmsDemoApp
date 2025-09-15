using FluentValidation;
using Mediator;
using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;

namespace SmsDemoApp.Application.Features.SMS.Command;


public record ProcessPreSMSCommand(
    Guid Id,
    int CustomerId,
    string Text,
    string ReciverPhoneNumber) : IRequest<OperationResult<bool>>, IValidatableModel<ProcessPreSMSCommand>
{
    public IValidator<ProcessPreSMSCommand> Validate(ValidationModelBase<ProcessPreSMSCommand> validator)
    {



        return validator;
    }
}