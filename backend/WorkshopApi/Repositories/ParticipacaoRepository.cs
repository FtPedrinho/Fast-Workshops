// Repository conversa com o banco de dados, por isso a importação do EntityFrameworkCore.
using Microsoft.EntityFrameworkCore;
using WorkshopApi.Database;
using WorkshopApi.Models;

namespace WorkshopApi.Repositories;

public class ParticipacaoRepository
{
    private readonly AppDbContext _context;

    public ParticipacaoRepository(AppDbContext context)
    {
        _context = context; // Injeção de dependência para passar o contexto para o banco de dados
    }

    // Consulta do workshop na participação.
    public async Task<IEnumerable<ParticipacaoModel>> GetByWorkshopIdAsync(
        int workshopId)
    {
        return await _context.Participacoes
            .AsNoTracking()
            .Include(participacao => participacao.Colaborador)
            .Where(participacao => participacao.WorkshopId == workshopId)
            .ToListAsync();
    }

    // Consulta do elemento na tabela participação.
    public async Task<ParticipacaoModel?> GetByIdsAsync(
        int workshopId,
        int colaboradorId)
    {
        return await _context.Participacoes
            .AsNoTracking()
            .FirstOrDefaultAsync(participacao =>
                participacao.WorkshopId == workshopId &&
                participacao.ColaboradorId == colaboradorId);
    }

    // Cria uma nova relação entre workshop e colaborador.
    public async Task<ParticipacaoModel> CreateAsync(
        ParticipacaoModel participacao)
    {
        await _context.Participacoes.AddAsync(participacao);
        await _context.SaveChangesAsync();

        return participacao;
    }

    // Deleta uma relação entre workshop e colaborador.
    public async Task DeleteAsync(ParticipacaoModel participacao)
    {
        _context.Participacoes.Remove(participacao);
        await _context.SaveChangesAsync();
    }
}