// Usaremos o entity framework, o que significa que o repository conversa com o banco de dados.
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Database;
using WorkshopApi.Models;

namespace WorkshopApi.Repositories;

public class ColaboradorRepository
{
    // O repository é responsável por conversar com o banco de dados, então ele precisa do contexto do banco de dados.
    private readonly AppDbContext _context;

    // Utilizaremos injeção de dependência para passar o contexto do banco de dados para o repository.
    public ColaboradorRepository(AppDbContext context)
    {
        _context = context;
    }

    // É referente a operação de leitura de todos os colaboradores do banco de dados.
    public async Task<IEnumerable<ColaboradorModel>> GetAllAsync()
    {
        return await _context.Colaboradores
            .AsNoTracking() // É usado para melhorar o desempenho.
            .ToListAsync(); // Executa a consulta de forma assíncrona e retorna uma lista de colaboradores.
    }

    // Operação que retorna um colaborador específico do banco de dados, baseado no id.
    public async Task<ColaboradorModel?> GetByIdAsync(int id)
    {
        return await _context.Colaboradores
            .AsNoTracking()
            .FirstOrDefaultAsync(colaborador => colaborador.Id == id);
    }

    // Operação que cria um novo colaborador no banco de dados.
    public async Task<ColaboradorModel> CreateAsync(
        ColaboradorModel colaborador)
    {
        await _context.Colaboradores.AddAsync(colaborador);
        await _context.SaveChangesAsync();

        return colaborador;
    }

    // Operação que atualiza um colaborador existente no banco de dados.
    public async Task UpdateAsync(ColaboradorModel colaborador)
    {
        _context.Colaboradores.Update(colaborador);
        await _context.SaveChangesAsync();
    }

    // Operação que deleta um colaborador existente no banco de dados.
    public async Task DeleteAsync(ColaboradorModel colaborador)
    {
        _context.Colaboradores.Remove(colaborador);
        await _context.SaveChangesAsync();
    }
}