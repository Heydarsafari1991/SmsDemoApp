using SmsDemoApp.Application.Contracts.User.Models;
using SmsDemoApp.Application.Features.User.Commands.Register;
using SmsDemoApp.Application.Features.User.Queries.PasswordLogin;
using SmsDemoApp.WebFramework.Common;
using SmsDemoApp.WebFramework.Models;
using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Adly.Api.Controllers.User.V1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/User")]
public class UserController(ISender sender) : BaseController
{
    /// <summary>
    /// Registers User With Provided Information
    /// </summary>
    /// <returns></returns>
    [HttpPost("Register")]
    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
    public virtual async Task<IActionResult> Register(RegisterUserCommand command,CancellationToken cancellationToken)
    =>base.OperationResult(await sender.Send(command,cancellationToken));
    
    [HttpPost("TokenRequest")]
    [ProducesResponseType(typeof(ApiResult<JwtAccessTokenModel>), StatusCodes.Status200OK)]
    public virtual async Task<IActionResult> TokenRequest(UserPasswordLoginQuery query, CancellationToken cancellationToken)
    => base.OperationResult(await sender.Send(query,cancellationToken));
}