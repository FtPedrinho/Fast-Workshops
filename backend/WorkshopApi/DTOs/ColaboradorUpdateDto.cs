using System.ComponentModel.DataAnnotations;

namespace WorkshopApi.DTOs;

public class ColaboradorUpdateDto
{
    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;
}