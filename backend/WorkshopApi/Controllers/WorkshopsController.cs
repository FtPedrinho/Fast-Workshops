using Microsoft.AspNetCore.Mvc;
using WorkshopApi.DTOs;
using WorkshopApi.Services;

namespace WorkshopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkshopsController : ControllerBase
{
    private readonly WorkshopService _workshopService;

    public WorkshopsController(
        WorkshopService workshopService)
    {
        _workshopService = workshopService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkshopDto>>> GetAll()
    {
        var workshops =
            await _workshopService.GetAllAsync();

        return Ok(workshops);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<WorkshopDto>> GetById(int id)
    {
        var workshop =
            await _workshopService.GetByIdAsync(id);

        if (workshop is null)
            return NotFound();

        return Ok(workshop);
    }

    [HttpPost]
    public async Task<ActionResult<WorkshopDto>> Create(
        WorkshopCreateDto workshopDto)
    {
        var workshop =
            await _workshopService.CreateAsync(workshopDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = workshop.Id },
            workshop);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        WorkshopUpdateDto workshopDto)
    {
        var updated =
            await _workshopService.UpdateAsync(
                id,
                workshopDto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _workshopService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}