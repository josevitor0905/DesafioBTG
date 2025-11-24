using System.ComponentModel.DataAnnotations;

namespace backend.Models;
public class Vacina
{
    [Key]
    public int Id {get; set; }
    public string Nome {get; set; } = null!;
    public List<Vacinacao> Aplicacoes {get; set; } = new();
}