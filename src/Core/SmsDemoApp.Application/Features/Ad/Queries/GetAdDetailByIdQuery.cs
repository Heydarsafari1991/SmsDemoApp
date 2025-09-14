using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;
using FluentValidation;
using Mediator;

namespace SmsDemoApp.Application.Features.Ad.Queries;

public record GetAdDetailByIdQuery(Guid AdId) 
    : IValidatableModel<GetAdDetailByIdQuery>,IRequest<OperationResult<GetAdDetailByIdQueryResult>>
{
    public IValidator<GetAdDetailByIdQuery> Validate(ValidationModelBase<GetAdDetailByIdQuery> validator)
    {
        validator.RuleFor(c => c.AdId)
            .NotEmpty();

        return validator;
    }
}