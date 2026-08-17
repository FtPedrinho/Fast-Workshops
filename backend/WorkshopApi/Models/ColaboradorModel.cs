// O Model é responsável por representar a entidade do banco de dados.
namespace WorkshopApi.Models;

public class ColaboradorModel
{
    // A propriedade Id é a chave primária da entidade e será gerada automaticamente.
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty; // Propriedade não nula.
}