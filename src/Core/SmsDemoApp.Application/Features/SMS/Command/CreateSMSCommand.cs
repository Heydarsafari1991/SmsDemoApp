using FluentValidation;
using Mediator;
using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;
using SmsDemoApp.Domain.Entities.Message;
using System.Text.Json.Serialization;

namespace SmsDemoApp.Application.Features.SMS.Command;


public record CreateSMSCommand(
    int CustomerId,
    string Text,
    string ReciverPhoneNumber,
    MessageDelivaryStatus Status) : IRequest<OperationResult<bool>>, IValidatableModel<CreateSMSCommand>
{
    public IValidator<CreateSMSCommand> Validate(ValidationModelBase<CreateSMSCommand> validator)
    {
        validator.RuleFor(c => c.CustomerId)
            .NotEmpty();
        validator.RuleFor(c => c.ReciverPhoneNumber)
         .NotEmpty();


        return validator;
    }
}