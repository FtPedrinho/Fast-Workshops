// Service precisa entender o que é um DTO, um Model e um entity.
using WorkshopApi.DTOs;
using WorkshopApi.Models;
using WorkshopApi.Repositories;

namespace WorkshopApi.Services;

public class ColaboradorService
{
    private readonly ColaboradorRepository _colaboradorRepository;

    // O service não conversa diretamente com o banco de dados, ele conversa com o Repository.
    public ColaboradorService(ColaboradorRepository colaboradorRepository)
    {
        _colaboradorRepository = colaboradorRepository;
    }

    // O service é responsável por mapear os Models para DTOs e vice-versa.
    public async Task<IEnumerable<ColaboradorDto>> GetAllAsync()
    {
        var colaboradores = await _colaboradorRepository.GetAllAsync();

        return colaboradores.Select(MapToDto); // Aplicação DRY para mapear cada colaborador para DTO.
    }

    public async Task<ColaboradorDto?> GetByIdAsync(int id)
    {
        var colaborador = await _colaboradorRepository.GetByIdAsync(id);

        return colaborador is null
            ? null
            : MapToDto(colaborador); 
            // O resultado pode ser nulo, então é necessário verificar antes de mapear para DTO.
    }

    // O service é responsável por mapear os Models para DTOs e vice-versa.
    public async Task<ColaboradorDto> CreateAsync(ColaboradorDto colaboradorDto)
    {
        var colaborador = new ColaboradorModel
        {
            Nome = colaboradorDto.Nome
        };

        var createdColaborador =
            await _colaboradorRepository.CreateAsync(colaborador);

        return MapToDto(createdColaborador);
    }

    // Entender se a atualização foi bem-sucedida ou não é responsabilidade do service.
    public async Task<bool> UpdateAsync(
        int id,
        ColaboradorDto colaboradorDto)
    {
        var colaborador = await _colaboradorRepository.GetByIdAsync(id);

        if (colaborador is null)
            return false;

        colaborador.Nome = colaboradorDto.Nome;

        await _colaboradorRepository.UpdateAsync(colaborador);

        return true;
    }

    // Mesma lógica do UpdateAsync.
    public async Task<bool> DeleteAsync(int id)
    {
        var colaborador = await _colaboradorRepository.GetByIdAsync(id);

        if (colaborador is null)
            return false;

        await _colaboradorRepository.DeleteAsync(colaborador);

        return true;
    }

    // Aqui é onde o service faz a conversão de Model para DTO.
    private static ColaboradorDto MapToDto(ColaboradorModel colaborador)
    {
        return new ColaboradorDto
        {
            Id = colaborador.Id,
            Nome = colaborador.Nome
        };
    }
}