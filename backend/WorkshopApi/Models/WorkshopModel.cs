// O Model é responsável por representar a entidade do banco de dados.
namespace WorkshopApi.Models;

public class WorkshopModel
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty; // Propriedade não nula.

    public DateOnly DataRealizacao { get; set; }

    public string? Descricao { get; set; } // Propriedade nula, pois a descrição do workshop é opcional.

    public ICollection<ParticipacaoModel> Participacoes { get; set; }
        = new List<ParticipacaoModel>(); // Lista de colaboradores.
}