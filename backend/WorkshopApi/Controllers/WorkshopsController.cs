// Importação das funcionalidades do ASP.NET Core MVC (Controller) e dos DTOs e Services do projeto.
using Microsoft.AspNetCore.Mvc;
using WorkshopApi.DTOs;
using WorkshopApi.Services;

namespace WorkshopApi.Controllers;

[ApiController]
[Route("api/[controller]")] // Define a rota base para o controlador, que será "api/workshops".
public class WorkshopsController : ControllerBase
{
    private readonly WorkshopService _workshopService;

    // Injeção de dependência do WorkshopService, que será usado para realizar operações relacionadas aos workshops.
    public WorkshopsController(WorkshopService workshopService)
    {
        _workshopService = workshopService; // O controller não conversa diretamente com o banco de dados, ele conversa com o Service.
    }

    [HttpGet] // GET: api/workshops
    public async Task<ActionResult<IEnumerable<WorkshopDto>>> GetAll()
    {
        var workshops = await _workshopService.GetAllAsync();

        return Ok(workshops);
    }

    [HttpGet("{id:int}")] // GET: api/workshops/{id} - O parâmetro "id" é do tipo inteiro, o que garante que a rota só será acessada com um número inteiro.
    public async Task<ActionResult<WorkshopDto>> GetById(int id)
    {
        var workshop = await _workshopService.GetByIdAsync(id);

        if (workshop is null)
        {
            return NotFound();
        }

        return Ok(workshop);
    }

    [HttpPost] // POST: api/workshops  
    public async Task<ActionResult<WorkshopDto>> Create(WorkshopDto workshopDto)
    {
        var workshop = await _workshopService.CreateAsync(workshopDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = workshop.Id },
            workshop);
    }

    [HttpPut("{id:int}")] // PUT: api/workshops/{id} - O parâmetro "id" é do tipo inteiro, o que garante que a rota só será acessada com um número inteiro.
    public async Task<IActionResult> Update(
        int id,
        WorkshopDto workshopDto)
    {
        var updated = await _workshopService.UpdateAsync(id, workshopDto);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")] // DELETE: api/workshops/{id} - O parâmetro "id" é do tipo inteiro, o que garante que a rota só será acessada com um número inteiro.
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _workshopService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}