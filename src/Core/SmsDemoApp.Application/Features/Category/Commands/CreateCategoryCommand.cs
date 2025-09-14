using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;
using FluentValidation;
using Mediator;

namespace SmsDemoApp.Application.Features.Category.Commands;

public record CreateCategoryCommand(string CategoryName)
    : IRequest<OperationResult<bool>>, IValidatableModel<CreateCategoryCommand>
{
    public IValidator<CreateCategoryCommand> Validate(ValidationModelBase<CreateCategoryCommand> validator)
    {
        validator.RuleFor(c => c.CategoryName)
            .NotEmpty();

        return validator;
    }
}