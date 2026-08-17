using Microsoft.AspNetCore.Mvc;
using WorkshopApi.DTOs;
using WorkshopApi.Services;

namespace WorkshopApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ColaboradoresController : ControllerBase
{
    private readonly ColaboradorService _colaboradorService;

    public ColaboradoresController(
        ColaboradorService colaboradorService)
    {
        _colaboradorService = colaboradorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ColaboradorDto>>> GetAll()
    {
        var colaboradores =
            await _colaboradorService.GetAllAsync();

        return Ok(colaboradores);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ColaboradorDto>> GetById(int id)
    {
        var colaborador =
            await _colaboradorService.GetByIdAsync(id);

        if (colaborador is null)
            return NotFound();

        return Ok(colaborador);
    }

    [HttpPost]
    public async Task<ActionResult<ColaboradorDto>> Create(
        ColaboradorCreateDto colaboradorDto)
    {
        var colaborador =
            await _colaboradorService.CreateAsync(colaboradorDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = colaborador.Id },
            colaborador);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        ColaboradorUpdateDto colaboradorDto)
    {
        var updated =
            await _colaboradorService.UpdateAsync(
                id,
                colaboradorDto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted =
            await _colaboradorService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}