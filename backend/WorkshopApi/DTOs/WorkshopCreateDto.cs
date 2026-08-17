using System.ComponentModel.DataAnnotations;

namespace WorkshopApi.DTOs;

public class WorkshopCreateDto
{
    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    public DateOnly DataRealizacao { get; set; }

    [StringLength(1000)]
    public string? Descricao { get; set; }
}