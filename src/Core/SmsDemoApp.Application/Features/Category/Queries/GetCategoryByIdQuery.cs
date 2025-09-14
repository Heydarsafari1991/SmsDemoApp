using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;
using FluentValidation;
using Mediator;

namespace SmsDemoApp.Application.Features.Category.Queries;

public record GetCategoryByIdQuery(Guid CategoryId) : IRequest<OperationResult<GetCategoryByIdQueryResult>>,
    IValidatableModel<GetCategoryByIdQuery>
{
    public IValidator<GetCategoryByIdQuery> Validate(ValidationModelBase<GetCategoryByIdQuery> validator)
    {
        validator.RuleFor(c => c.CategoryId)
            .NotEmpty();

        return validator;
    }
}