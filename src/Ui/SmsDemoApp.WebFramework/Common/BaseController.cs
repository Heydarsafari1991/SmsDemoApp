using System.Security.Claims;
using SmsDemoApp.Application.Common;
using SmsDemoApp.WebFramework.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SmsDemoApp.WebFramework.Common;

public class BaseController:ControllerBase
{
    protected string? UserName => base.User.Identity?.Name;

    protected Guid? UserId => Guid.Parse(User.Identity?.GetUserId()!);

    protected string? UserEmail => User.Identity?.FindFirstValue(ClaimTypes.Email);

    protected string? UserKey => User.Identity?.FindFirstValue(ClaimTypes.UserData);


    protected IActionResult OperationResult<TModel>(OperationResult<TModel> result)
    {
        if (result.IsSuccess)
            return result.Result is bool ? Ok() : Ok(result.Result);

        if (result.IsNotFound)
        {
            AddErrors(result);

            var errors = new ValidationProblemDetails(ModelState);

            return NotFound(errors.Errors);
        }
        
        AddErrors(result);
        
        var badRequestErrors = new ValidationProblemDetails(ModelState);

        return BadRequest(badRequestErrors);
    }

    private void AddErrors<TModel>(OperationResult<TModel> result)
    {
        foreach (var errorMessage in result.ErrorMessages)
        {
            ModelState.AddModelError(errorMessage.Key,errorMessage.Value);
        }
    }
}