using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sds.DentistChair.Domain.Models.ChairAggregate.Dtos;
using Sds.DentistChair.Domain.Models.ChairAggregate.Services;
using Sds.DentistChair.Domain.Notifier;
using System.Net;

namespace Sds.DentistChair.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/allocation")]
public class AllocationController(
        ILogger<AllocationController> logger,
        IChairService chairService,
        INotifierMessage notifierMessage) : MainController(notifierMessage)
{

    [HttpPost("allocate")]
    public async Task<ActionResult> AllocateChairs([FromBody] AllocationRequest request)
    {
        var chairs = await chairService.GetChairs().ToArrayAsync();
        if (chairs == null || chairs.Length == 0)
        {
            logger.LogError("No chairs available.");
            notifierMessage.Add("No chairs available.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        if (!await chairService.Allocate(request, chairs))
            return CustomResponse(HttpStatusCode.BadRequest);

        return CustomResponse(HttpStatusCode.OK);
    }

}
