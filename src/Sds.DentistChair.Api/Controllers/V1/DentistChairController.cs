using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sds.DentistChair.Domain.Models.ChairAggregate.Dtos;
using Sds.DentistChair.Domain.Models.ChairAggregate.Entities;
using Sds.DentistChair.Domain.Models.ChairAggregate.Services;
using Sds.DentistChair.Domain.Notifier;
using System.Net;

namespace Sds.DentistChair.Api.Controllers.V1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/chair")]
public class ChairController(
    ILogger<ChairController> logger,
    IChairService chairService,
    IMapper mapper,
    INotifierMessage notifierMessage) : MainController(notifierMessage)
{
    [HttpGet("get-chairs")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult GetChairs()
    {
        var chairs = chairService.GetChairs();

        if (chairs == null)
        {
            notifierMessage.Add("There are no chairs");
            logger.LogError("There are no chairs");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse(HttpStatusCode.OK, chairs.ToArray());
    }

    [HttpGet("get-chair/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult GetChair(int id)
    {
        var chair = chairService.GetChair(id);
        if (chair == null)
        {
            notifierMessage.Add("Chair does not exist");
            logger.LogError("Chair does not exist");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse(HttpStatusCode.OK, chair);
    }

    [HttpPost("save")]
    public async Task<ActionResult> Save(ChairDto chairDto)
    {

        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var chair = mapper.Map<Chair>(chairDto);

        if (!await chairService.SaveChair(chair))
            return CustomResponse(HttpStatusCode.BadRequest);

        return CreatedAtAction("GetChair", new { id = chair.Id }, chair);
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult> Update(int id, ChairDto chairDto)
    {
        if (!ModelState.IsValid || id != chairDto.Id)
            return CustomResponse(ModelState);

        try
        {
            var chair = mapper.Map<Chair>(chairDto);

            if (!await chairService.UpdateChair(chair))
                return CustomResponse(HttpStatusCode.NotModified);

            return CustomResponse(HttpStatusCode.OK);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ChairExists(id))
                return CustomResponse(HttpStatusCode.NotFound);
            else
                throw;
        }
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteChair(int id)
    {
        var removed = await chairService.DeleteChair(id);

        if (!removed)
            return CustomResponse(HttpStatusCode.BadRequest);

        return CustomResponse(HttpStatusCode.OK);
    }

    private bool ChairExists(int id) => chairService.GetChair(id) != null;
}
