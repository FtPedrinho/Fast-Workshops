// Importação de dependências.
using WorkshopApi.DTOs;
using WorkshopApi.Models;
using WorkshopApi.Repositories;

namespace WorkshopApi.Services;

public class ParticipacaoService
{
    // Service depende dos repositories de colaboradores, participação e workshops.
    private readonly ParticipacaoRepository _participacaoRepository;
    private readonly ColaboradorRepository _colaboradorRepository;
    private readonly WorkshopRepository _workshopRepository;

    public ParticipacaoService(
        ParticipacaoRepository participacaoRepository,
        ColaboradorRepository colaboradorRepository,
        WorkshopRepository workshopRepository)
    {
        _participacaoRepository = participacaoRepository;
        _colaboradorRepository = colaboradorRepository;
        _workshopRepository = workshopRepository;
    }

    public async Task<IEnumerable<ParticipacaoDto>> GetByWorkshopIdAsync(
        int workshopId)
    {
        var participacoes =
            await _participacaoRepository.GetByWorkshopIdAsync(workshopId);

        return participacoes.Select(MapToDto); 
    }

    // Operação de criar uma nova participação (ele verifica se a existencia das entidades e se a relação já existe)
    public async Task<ParticipacaoDto?> CreateAsync(
        int workshopId,
        int colaboradorId)
    {
        var workshop =
            await _workshopRepository.GetByIdAsync(workshopId);

        if (workshop is null)
            return null;

        var colaborador =
            await _colaboradorRepository.GetByIdAsync(colaboradorId);

        if (colaborador is null)
            return null;

        var participacaoExistente =
            await _participacaoRepository.GetByIdsAsync(
                workshopId,
                colaboradorId);

        if (participacaoExistente is not null)
            return null;

        var participacao = new ParticipacaoModel
        {
            WorkshopId = workshopId,
            ColaboradorId = colaboradorId
        };

        var createdParticipacao =
            await _participacaoRepository.CreateAsync(participacao);

        return MapToDto(createdParticipacao);
    }

    // Método voltado para a operação de deletar
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

    // Services mapeia o Model para DTO, utilizando colaborador e workshop.
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