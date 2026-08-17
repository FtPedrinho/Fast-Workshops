// O DTO (Data Transfer Object) é responsável por transferir dados entre camadas da aplicação.
namespace WorkshopApi.DTOs;

public class ColaboradorDto
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty; // Propriedade não nula.
}