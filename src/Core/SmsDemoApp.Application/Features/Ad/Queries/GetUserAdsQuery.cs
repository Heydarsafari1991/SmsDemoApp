using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;
using SmsDemoApp.Domain.Entities.Ad;
using FluentValidation;
using Mediator;

namespace SmsDemoApp.Application.Features.Ad.Queries;

public record GetUserAdsQuery(Guid UserId)
    : IValidatableModel<GetUserAdsQuery>, IRequest<OperationResult<List<GetUserAdsQueryResult>>>
{
    public IValidator<GetUserAdsQuery> Validate(ValidationModelBase<GetUserAdsQuery> validator)
    {
        validator.RuleFor(c => c.UserId)
            .NotEmpty();

        return validator;
    }
}