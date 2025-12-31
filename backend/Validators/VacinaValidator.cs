using FluentValidation;
using backend.DTOs;
using System.Text.RegularExpressions;
namespace backend.Validators;

using backend.Models;
using backend.Services;

public class VacinaValidator : AbstractValidator<VacinaCreateDTO>
{
    private readonly VacinaService _vacinaService;
    public VacinaValidator(VacinaService vacinaService)
    {
        _vacinaService = vacinaService;

        RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome da vacina é obrigatório.")
        .Must(ValidarSeSoLetras).WithMessage("O nome da vacina não deve conter números.")
        .MustAsync(async (nome, cancellation) => 
                {
                    bool existe = await _vacinaService.ExisteVacinaComNomeAsync(nome);
                    return !existe;
                })
                .WithMessage("Esta vacina já está cadastrada no sistema.");
    }

    private bool ValidarSeSoLetras(string nome)
    {
        if (Regex.IsMatch(nome, "[0-9]"))
        {
            return false;
        }

        return true;
    }
}