// Usaremos o entity framework, o que significa que o repository conversa com o banco de dados.
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Database;
using WorkshopApi.Models;

namespace WorkshopApi.Repositories;

public class WorkshopRepository
{
    // O repository é responsável por conversar com o banco de dados, então ele precisa do contexto do banco de dados.
    private readonly AppDbContext _context;

    // Utilizaremos injeção de dependência para passar o contexto do banco de dados para o repository.
    public WorkshopRepository(AppDbContext context)
    {
        _context = context;
    }

    // É referente a operação de leitura de todos os workkshops do banco de dados.
    public async Task<IEnumerable<WorkshopModel>> GetAllAsync()
    {
        return await _context.Workshops
            .AsNoTracking() // Executado para melhorar desempenho.
            .Include(workshop => workshop.Participacoes)
            .ThenInclude(participacao => participacao.Colaborador)
            .ToListAsync(); // Executa a consulta de forma assíncrona e retorna uma lista de workshops.
    }

    // Operação que retorna um workshop específico do banco de dados, baseado no id.

    public async Task<WorkshopModel?> GetByIdAsync(int id)
    {
        return await _context.Workshops
            .AsNoTracking()
            .Include(workshop => workshop.Participacoes)
            .ThenInclude(participacao => participacao.Colaborador)
            .FirstOrDefaultAsync(workshop => workshop.Id == id);
    }

    // Operação que cria um novo workshop no banco de dados.
    public async Task<WorkshopModel> CreateAsync(WorkshopModel workshop)
    {
        await _context.Workshops.AddAsync(workshop);
        await _context.SaveChangesAsync();

        return workshop;
    }

    // Operação que atualiza um worksho existente no banco de dados.
    public async Task UpdateAsync(WorkshopModel workshop)
    {
        _context.Workshops.Update(workshop);
        await _context.SaveChangesAsync();
    }

    // Operação que deleta um worksho existente no banco de dados.
    public async Task DeleteAsync(WorkshopModel workshop)
    {
        _context.Workshops.Remove(workshop);
        await _context.SaveChangesAsync();
    }
}