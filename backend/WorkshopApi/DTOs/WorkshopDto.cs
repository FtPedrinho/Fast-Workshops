// O DTO (Data Transfer Object) é responsável por representar a entidade que será enviada para o cliente.
namespace WorkshopApi.DTOs;

public class WorkshopDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty; // Propriedade NÃO nula.

    public DateOnly DataRealizacao { get; set; }

    public string? Descricao { get; set; } // Propriedade nula.

    public IEnumerable<ColaboradorDto> Participantes { get; set; }
        = Enumerable.Empty<ColaboradorDto>();
}