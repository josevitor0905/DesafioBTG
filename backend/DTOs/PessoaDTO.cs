namespace backend.DTOs;

public class PessoaDTO
{
    public int Id {get; set; }
    public string Nome {get; set; } = string.Empty;
    public int Idade {get;set; }
    public string Sexo{get; set; } = string.Empty;
}