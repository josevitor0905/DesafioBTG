namespace backend.DTOs;

public class VacinacaoCreateDTO
{
    public int PessoaId {get; set; }
    public int VacinaId {get; set; }
    public int Dose {get; set; }
    public DateTime DataAplicacao {get; set; }
}