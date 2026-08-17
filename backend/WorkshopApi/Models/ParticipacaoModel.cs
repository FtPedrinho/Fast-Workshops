// O model é usado para representar as entidades do banco de dados.
namespace WorkshopApi.Models;

public class ParticipacaoModel
{
    public int Id { get; set; }

    public int ColaboradorId { get; set; } // Id do colaborador.

    public int WorkshopId { get; set; } // Id do workshop.

    public ColaboradorModel Colaborador { get; set; } = null!; // representa o colaborador relacionado.

    public WorkshopModel Workshop { get; set; } = null!; // Representa o workshop relacionado.
}