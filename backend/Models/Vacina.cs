using System.ComponentModel.DataAnnotations;

namespace backend.Models;
public class Vacina
{
    public int Id {get; set; }
    public string Nome {get; set; } = null!;
    public List<Vacinacao> Aplicacoes {get; set; } = new();
}