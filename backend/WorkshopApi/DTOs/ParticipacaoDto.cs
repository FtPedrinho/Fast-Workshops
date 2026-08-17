namespace WorkshopApi.DTOs;

public class ParticipacaoDto
{
    // Não precisa registrar Id próprio. Apenas reconhecer os ids colaborador e o workshop é suficiente.
    public int ColaboradorId { get; set; }

    public int WorkshopId { get; set; }
}