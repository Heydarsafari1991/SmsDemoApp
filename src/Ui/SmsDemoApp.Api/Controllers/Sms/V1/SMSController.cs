
using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmsDemoApp.Application.Features.SMS.Command;
using SmsDemoApp.Application.Features.SMS.Queries;
using SmsDemoApp.WebFramework.Common;
using SmsDemoApp.WebFramework.Models;

namespace SmsDemoApp.Api.Controllers.Sms.V1
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/SMS")]
    public class SMSController(ISender sender) : BaseController
    {

        [HttpPost("Sms.ASync")]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SMSASync(CreatePreSMSCommand model, CancellationToken cancellationToken)
       => base.OperationResult(await sender.Send(
           model, cancellationToken));
        [HttpPost("Sms.Sync")]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> SMSSync(CreateSMSCommand model, CancellationToken cancellationToken)
     => base.OperationResult(await sender.Send(
         model, cancellationToken));

        /// <summary>
        /// Gets a list of user ads
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("CustomerSMSs")]
        [ProducesResponseType(typeof(ApiResult<GetCustomerSMSsQueryResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerSMSs(GetCustomerSMSsQuery query ,CancellationToken cancellationToken)
        => base.OperationResult(await sender.Send(query, cancellationToken));

    }
}
