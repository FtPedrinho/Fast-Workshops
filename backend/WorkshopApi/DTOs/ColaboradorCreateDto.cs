using System.ComponentModel.DataAnnotations;

namespace WorkshopApi.DTOs;

public class ColaboradorCreateDto
{
    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;
}