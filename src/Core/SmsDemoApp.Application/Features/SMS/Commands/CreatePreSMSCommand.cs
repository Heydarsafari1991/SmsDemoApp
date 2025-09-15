using FluentValidation;
using Mediator;
using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;

namespace SmsDemoApp.Application.Features.SMS.Command;

public record CreatePreSMSCommand(
    int CustomerId,
    string Text,
    string ReciverPhoneNumber) : IRequest<OperationResult<bool>>, IValidatableModel<CreatePreSMSCommand>
{
    public IValidator<CreatePreSMSCommand> Validate(ValidationModelBase<CreatePreSMSCommand> validator)
    {
        validator.RuleFor(c => c.CustomerId)
            .NotEmpty();
        validator.RuleFor(c => c.ReciverPhoneNumber)
         .NotEmpty();


        return validator;
    }
}


