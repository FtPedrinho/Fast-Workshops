using WorkshopApi.DTOs;
using WorkshopApi.Models;
using WorkshopApi.Repositories;

namespace WorkshopApi.Services;

public class ColaboradorService
{
    private readonly ColaboradorRepository _colaboradorRepository;

    // Injeção de dependências em Repository. Ou seja, Service não se comunica com o banco de dados.
    public ColaboradorService(ColaboradorRepository colaboradorRepository)
    {
        _colaboradorRepository = colaboradorRepository;
    }

    // Operação para consultar todos os colaboradores.
    public async Task<IEnumerable<ColaboradorDto>> GetAllAsync()
    {
        var colaboradores = await _colaboradorRepository.GetAllAsync();

        return colaboradores.Select(MapToDto);
    }

    // Operação de consultar colaborador por Id.
    public async Task<ColaboradorDto?> GetByIdAsync(int id)
    {
        var colaborador = await _colaboradorRepository.GetByIdAsync(id);

        return colaborador is null
            ? null
            : MapToDto(colaborador);
    }

    // operação de criar um novo colaborador.
    public async Task<ColaboradorDto> CreateAsync(
        ColaboradorCreateDto colaboradorDto)
    {
        var colaborador = new ColaboradorModel
        {
            Nome = colaboradorDto.Nome.Trim()
        };

        var createdColaborador =
            await _colaboradorRepository.CreateAsync(colaborador);

        return MapToDto(createdColaborador);
    }

    // Operação de atualizar um colaborador existente.
    public async Task<bool> UpdateAsync(
        int id,
        ColaboradorUpdateDto colaboradorDto)
    {
        var colaborador =
            await _colaboradorRepository.GetByIdAsync(id);

        if (colaborador is null)
            return false;

        colaborador.Nome = colaboradorDto.Nome.Trim();

        await _colaboradorRepository.UpdateAsync(colaborador);

        return true;
    }

    // Deleta um colaborador existente.
    public async Task<bool> DeleteAsync(int id)
    {
        var colaborador =
            await _colaboradorRepository.GetByIdAsync(id);

        if (colaborador is null)
            return false;

        await _colaboradorRepository.DeleteAsync(colaborador);

        return true;
    }

    // Service faz a conversão de Model para DTO.
    private static ColaboradorDto MapToDto(
        ColaboradorModel colaborador)
    {
        return new ColaboradorDto
        {
            Id = colaborador.Id,
            Nome = colaborador.Nome
        };
    }
}