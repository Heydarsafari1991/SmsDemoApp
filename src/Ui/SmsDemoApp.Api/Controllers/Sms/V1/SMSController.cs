using Adly.Api.Models.Ad;
using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmsDemoApp.Application.Features.Ad.Commands;
using SmsDemoApp.Application.Features.Location.Queries;
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
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<List<GetLocationByIdQueryResult>>), StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> GetLocation(Guid locationId, CancellationToken cancellationToken)
    => base.OperationResult(await sender.Send(new GetLocationByIdQuery(locationId), cancellationToken));

        /// <summary>
        /// Gets a list of location based on name provided
        /// </summary>
        [HttpGet("Search")]
        [ProducesResponseType(typeof(ApiResult<List<GetLocationByNameQueryResult>>), StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> SearchLocations(string name, CancellationToken cancellationToken)
            => base.OperationResult(await sender.Send(new GetLocationByNameQuery(name), cancellationToken));

        [HttpPost("Create")]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreatePreSMSCommand model, CancellationToken cancellationToken)
       => base.OperationResult(await sender.Send(
           model, cancellationToken));
    }
}
