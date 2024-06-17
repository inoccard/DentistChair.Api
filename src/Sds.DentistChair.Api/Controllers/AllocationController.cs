using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sds.DentistChair.Domain.Models.ChairAggregate.Dtos;
using Sds.DentistChair.Domain.Models.ChairAggregate.Services;

namespace Sds.DentistChair.Api.Controllers;

public class AllocationController(
        ILogger<AllocationController> logger,
        IChairService chairService) : ControllerBase
{

    [HttpPost("allocate")]
    public async Task<ActionResult> AllocateChairs([FromBody] AllocationRequest request)
    {
        var chairs = await chairService.GetChairs().ToArrayAsync();
        if (chairs == null || chairs.Length == 0)
            return NotFound("No chairs available.");

        var alocated = await chairService.Allocate(request, chairs);

        return Ok();
    }

}
