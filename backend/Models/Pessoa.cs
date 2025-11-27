namespace backend.Models;
public class Pessoa
{
    public int Id {get; set; }
    public string Nome {get; set; } = null!;
    public int Idade {get;set; }
    public string Sexo{get; set; } = null!;
    public List<Vacinacao> Vacinacoes {get; set; } = new();
}