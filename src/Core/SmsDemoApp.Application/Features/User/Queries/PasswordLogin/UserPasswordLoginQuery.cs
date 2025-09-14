using SmsDemoApp.Application.Common;
using SmsDemoApp.Application.Common.Validation;
using SmsDemoApp.Application.Contracts.User.Models;
using FluentValidation;
using Mediator;

namespace SmsDemoApp.Application.Features.User.Queries.PasswordLogin;

public record UserPasswordLoginQuery(string UserNameOrEmail, string Password)
    : IRequest<OperationResult<JwtAccessTokenModel>>, IValidatableModel<UserPasswordLoginQuery>
{
    public IValidator<UserPasswordLoginQuery> Validate(ValidationModelBase<UserPasswordLoginQuery> validator)
    {
        validator.RuleFor(c => c.UserNameOrEmail)
            .NotEmpty();

        validator.RuleFor(c => c.Password)
            .NotEmpty();

        return validator;
    }
}