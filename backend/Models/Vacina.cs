namespace backend.Models;
public class Vacina
{
    public int Id {get; set; }
    public string Name {get; set; } = null!;
    public List<Vacinacao> Aplicacoes {get; set; } = new();
}