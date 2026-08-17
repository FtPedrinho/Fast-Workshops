using WorkshopApi.DTOs;
using WorkshopApi.Models;
using WorkshopApi.Repositories;

namespace WorkshopApi.Services;

public class WorkshopService
{
    private readonly WorkshopRepository _workshopRepository;

    // Injeção de dependências em Repository. Ou seja, Service não se comunica com o banco de dados.
    public WorkshopService(WorkshopRepository workshopRepository)
    {
        _workshopRepository = workshopRepository;
    }

    // Consulta de todos os workshops existente.
    public async Task<IEnumerable<WorkshopDto>> GetAllAsync()
    {
        var workshops = await _workshopRepository.GetAllAsync();

        return workshops.Select(MapToDto);
    }

    // Consulta do workshop existente por id.
    public async Task<WorkshopDto?> GetByIdAsync(int id)
    {
        var workshop = await _workshopRepository.GetByIdAsync(id);

        return workshop is null
            ? null
            : MapToDto(workshop);
    }

    // cadastra um novo workshop.
    public async Task<WorkshopDto> CreateAsync(
        WorkshopCreateDto workshopDto)
    {
        var workshop = new WorkshopModel
        {
            Nome = workshopDto.Nome.Trim(),
            DataRealizacao = workshopDto.DataRealizacao,
            Descricao = workshopDto.Descricao?.Trim()
        };

        var createdWorkshop =
            await _workshopRepository.CreateAsync(workshop);

        return MapToDto(createdWorkshop);
    }

    // Atualiza um workshop existente.
    public async Task<bool> UpdateAsync(
        int id,
        WorkshopUpdateDto workshopDto)
    {
        var workshop =
            await _workshopRepository.GetByIdAsync(id);

        if (workshop is null)
            return false;

        workshop.Nome = workshopDto.Nome.Trim();
        workshop.DataRealizacao = workshopDto.DataRealizacao;
        workshop.Descricao = workshopDto.Descricao?.Trim();

        await _workshopRepository.UpdateAsync(workshop);

        return true;
    }

    // Deleta um workshop existente.
    public async Task<bool> DeleteAsync(int id)
    {
        var workshop =
            await _workshopRepository.GetByIdAsync(id);

        if (workshop is null)
            return false;

        await _workshopRepository.DeleteAsync(workshop);

        return true;
    }

    // Mapeia de Model para DTO
    private static WorkshopDto MapToDto(
        WorkshopModel workshop)
    {
        return new WorkshopDto
        {
            Id = workshop.Id,
            Nome = workshop.Nome,
            DataRealizacao = workshop.DataRealizacao,
            Descricao = workshop.Descricao,

            Participantes = workshop.Participacoes
                .Select(participacao => new ColaboradorDto
                {
                    Id = participacao.Colaborador.Id,
                    Nome = participacao.Colaborador.Nome
                })
                .ToList()
        };
    }
}