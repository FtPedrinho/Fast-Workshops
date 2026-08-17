using System.ComponentModel.DataAnnotations;

namespace WorkshopApi.DTOs;

public class ParticipacaoCreateDto
{
    [Required]
    public int ColaboradorId { get; set; }
}