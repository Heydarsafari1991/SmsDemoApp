using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;
using FluentValidation;
using Mediator;

namespace SmsDemoApp.Application.Features.Category.Queries;

public record GetCategoriesByNameQuery(string CategoryName)
    : IRequest<OperationResult<List<GetCategoriesByNameQueryResult>>>
        , IValidatableModel<GetCategoriesByNameQuery>
{
    public IValidator<GetCategoriesByNameQuery> Validate(ValidationModelBase<GetCategoriesByNameQuery> validator)
    {
        validator.RuleFor(c => c.CategoryName)
            .NotEmpty();

        return validator;
    }
}