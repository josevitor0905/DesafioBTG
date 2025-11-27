namespace backend.DTOs;

public class VacinacaoDTO
{
    public int Id {get; set; }
    public string NomeVacina {get; set; } = string.Empty;
    public int Dose {get; set; }
    public DateTime DataAplicacao {get; set; }
}