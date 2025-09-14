using SmsDemoApp.Application.Contracts.User.Models;
using SmsDemoApp.Domain.Entities.User;

namespace SmsDemoApp.Application.Contracts.User;

public interface IJwtService
{
    Task<JwtAccessTokenModel> GenerateTokenAsync(UserEntity user, CancellationToken cancellationToken);
}