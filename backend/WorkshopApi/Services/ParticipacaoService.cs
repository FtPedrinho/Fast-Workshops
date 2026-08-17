using WorkshopApi.DTOs;
using WorkshopApi.Models;
using WorkshopApi.Repositories;

namespace WorkshopApi.Services;

public class ParticipacaoService
{
    private readonly ParticipacaoRepository _participacaoRepository;
    private readonly ColaboradorRepository _colaboradorRepository;
    private readonly WorkshopRepository _workshopRepository;

    // Injeção de dependências com todos os repositorios.

    public ParticipacaoService(
        ParticipacaoRepository participacaoRepository,
        ColaboradorRepository colaboradorRepository,
        WorkshopRepository workshopRepository)
    {
        _participacaoRepository = participacaoRepository;
        _colaboradorRepository = colaboradorRepository;
        _workshopRepository = workshopRepository;
    }

    // Busca por participações no workshop Id.
    public async Task<IEnumerable<ParticipacaoDto>> GetByWorkshopIdAsync(
        int workshopId)
    {
        var participacoes =
            await _participacaoRepository.GetByWorkshopIdAsync(workshopId);

        return participacoes.Select(MapToDto);
    }

    // Criação de uma nova participação
    public async Task<(ParticipacaoCreateResult Result, ParticipacaoDto? Participacao)> CreateAsync(
        int workshopId,
        int colaboradorId)
    {
        var workshop =
            await _workshopRepository.GetByIdAsync(workshopId);

        if (workshop is null)
            return (ParticipacaoCreateResult.WorkshopNotFound, null);

        var colaborador =
            await _colaboradorRepository.GetByIdAsync(colaboradorId);

        if (colaborador is null)
            return (ParticipacaoCreateResult.ColaboradorNotFound, null);

        var participacaoExistente =
            await _participacaoRepository.GetByIdsAsync(
                workshopId,
                colaboradorId);

        if (participacaoExistente is not null)
            return (ParticipacaoCreateResult.AlreadyExists, null);

        var participacao = new ParticipacaoModel
        {
            WorkshopId = workshopId,
            ColaboradorId = colaboradorId
        };

        var created =
            await _participacaoRepository.CreateAsync(participacao);

        var dto = MapToDto(created);

        return (ParticipacaoCreateResult.Created, dto);
    }

    // Deletando uma participação.
    public async Task<bool> DeleteAsync(
        int workshopId,
        int colaboradorId)
    {
        var participacao =
            await _participacaoRepository.GetByIdsAsync(
                workshopId,
                colaboradorId);

        if (participacao is null)
            return false;

        await _participacaoRepository.DeleteAsync(participacao);

        return true;
    }

    // Service mapeia Model para DTO.
    private static ParticipacaoDto MapToDto(
        ParticipacaoModel participacao)
    {
        return new ParticipacaoDto
        {
            ColaboradorId = participacao.ColaboradorId,
            WorkshopId = participacao.WorkshopId
        };
    }
}