using Adly.Api.Models.Ad;
using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmsDemoApp.Application.Features.SMS.Command;
using SmsDemoApp.WebFramework.Common;
using SmsDemoApp.WebFramework.Models;

namespace SmsDemoApp.Api.Controllers.Sms.V1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/SMS")]
    public class SMSController(ISender sender) : BaseController
    {

        [HttpPost("Create")]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreatePreSMSCommand model, CancellationToken cancellationToken)
       => base.OperationResult(await sender.Send(
           model, cancellationToken));
    }
}
