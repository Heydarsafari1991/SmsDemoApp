using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;
using FluentValidation;
using Mediator;

namespace SmsDemoApp.Application.Features.Location.Commands;

public record CreateLocationCommand(string LocationName)
    : IRequest<OperationResult<bool>>, IValidatableModel<CreateLocationCommand>
{
    public IValidator<CreateLocationCommand> Validate(ValidationModelBase<CreateLocationCommand> validator)
    {
        validator.RuleFor(c => c.LocationName)
            .NotEmpty();

        return validator;
    }
}