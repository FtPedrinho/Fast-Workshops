// Importação de dependências e Comandos Controller da ASP.NET.
using Microsoft.AspNetCore.Mvc;
using WorkshopApi.DTOs;
using WorkshopApi.Services;

namespace WorkshopApi.Controllers;

[ApiController]
[Route("api/workshops/{workshopId:int}/participacoes")]
public class ParticipacoesController : ControllerBase
{
    private readonly ParticipacaoService _participacaoService;

    // Injeção de dependência em Service.
    public ParticipacoesController(
        ParticipacaoService participacaoService)
    {
        _participacaoService = participacaoService;
    }


    [HttpGet] // Equivalente à consulta a partir do workshop.
    public async Task<ActionResult<IEnumerable<ParticipacaoDto>>> GetByWorkshop(
        int workshopId)
    {
        var participacoes =
            await _participacaoService.GetByWorkshopIdAsync(workshopId);

        return Ok(participacoes);
    }

    [HttpPost]
    public async Task<ActionResult<ParticipacaoDto>> Create(
        int workshopId,
        ParticipacaoCreateDto participacaoDto)
    {
        var (result, participacao) =
            await _participacaoService.CreateAsync(
                workshopId,
                participacaoDto.ColaboradorId);

        return result switch
        {
            ParticipacaoCreateResult.WorkshopNotFound =>
                NotFound(new
                {
                    message = "Workshop não encontrado."
                }),

            ParticipacaoCreateResult.ColaboradorNotFound =>
                NotFound(new
                {
                    message = "Colaborador não encontrado."
                }),

            ParticipacaoCreateResult.AlreadyExists =>
                Conflict(new
                {
                    message = "O colaborador já está registrado neste workshop."
                }),

            ParticipacaoCreateResult.Created =>
                Created(
                    $"/api/workshops/{workshopId}/participacoes/{participacao!.ColaboradorId}",
                    participacao),

            _ => StatusCode(500)
        };
    }

    [HttpDelete("{colaboradorId:int}")] // Deletando uma participação.
    public async Task<IActionResult> Delete(
        int workshopId,
        int colaboradorId)
    {
        var deleted = await _participacaoService.DeleteAsync(
            workshopId,
            colaboradorId);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Participação não encontrada."
            });
        }

        return NoContent();
    }
}