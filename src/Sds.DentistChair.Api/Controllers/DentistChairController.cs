using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sds.DentistChair.Domain.Models.ChairAggregate.Entities;
using Sds.DentistChair.Domain.Models.ChairAggregate.Services;

namespace Sds.DentistChair.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DentistChairController(
    ILogger<DentistChairController> logger,
    IChairService chairService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult GetChairs()
    {
        var chairs = chairService.GetChairs();

        return Ok(chairs.ToArray());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult GetChair(int id)
    {
        var chair = chairService.GetChair(id);
        if (chair == null)
            return NotFound();

        return Ok(chair);
    }

    [HttpPost]
    public async Task<ActionResult> Save(Chair chair)
    {
        await chairService.SaveChair(chair);
        return CreatedAtAction("GetChair", new { id = chair.Id }, chair);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Chair chair)
    {
        if (id != chair.Id)
            return BadRequest();

        try
        {
            await chairService.UpdateChair(chair);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ChairExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteChair(int id)
    {
        var removed = await chairService.DeleteChair(id);

        if (!removed)
            return BadRequest();

        return NoContent();
    }

    private bool ChairExists(int id) => chairService.GetChair(id) != null;
}
