using System;
public class Vacinacao
{
    public int Id {get; set; }
    public int PessoaId {get; set; }
    public Pessoa? Pessoa {get; set; }
    public int VacinaId {get; set; }
    public Vacina? Vacina {get; set; }
    public int Dose {get; set; }
    public DateTime DataDaVacinacao { get; set; }
}