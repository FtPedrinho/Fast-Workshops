// permite usar Services e DTOs do projeto, além de ASP.NET core.
using Microsoft.AspNetCore.Mvc;
using WorkshopApi.DTOs;
using WorkshopApi.Services;

namespace WorkshopApi.Controllers;

[ApiController] // Indica que a classe é um controlador de API.
[Route("api/[controller]")] // Define a rota base para o controlador.
public class ColaboradoresController : ControllerBase
{
    private readonly ColaboradorService _colaboradorService;

    // O controller não conversa diretamente com o banco de dados, ele conversa com o Service.
    public ColaboradoresController(ColaboradorService colaboradorService)
    {
        _colaboradorService = colaboradorService;
    }

    // GET: api/colaboradores
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ColaboradorDto>>> GetAll()
    {
        var colaboradores = await _colaboradorService.GetAllAsync();

        return Ok(colaboradores);
    }

    // GET: api/colaboradores/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ColaboradorDto>> GetById(int id)
    {
        var colaborador = await _colaboradorService.GetByIdAsync(id);

        if (colaborador is null)
            return NotFound();

        return Ok(colaborador);
    }

    // POST: api/colaboradores
    [HttpPost]
    public async Task<ActionResult<ColaboradorDto>> Create(
        ColaboradorDto colaboradorDto)
    {
        var colaborador = await _colaboradorService.CreateAsync(colaboradorDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = colaborador.Id },
            colaborador);
    }

    // POST: api/colaboradores/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        ColaboradorDto colaboradorDto)
    {
        var updated = await _colaboradorService.UpdateAsync(
            id,
            colaboradorDto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/colaboradores/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _colaboradorService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}