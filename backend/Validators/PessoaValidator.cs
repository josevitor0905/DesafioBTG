using FluentValidation;
using backend.DTOs;
using System.Text.RegularExpressions;
namespace backend.Validators;
using backend.Services;

public class PessoaValidator : AbstractValidator<PessoaCreateDTO>
{
    private readonly PessoaService _pessoaService;
    public PessoaValidator(PessoaService pessoaService)
    {
        _pessoaService = pessoaService;

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome da pessoa é obrigatório.")
            .Matches(@"^[a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+$")
            .WithMessage("O nome da pessoa não deve conter números ou caracteres especiais.")
            .MustAsync(async (nome, cancellation) => 
                {
                    bool existe = await _pessoaService.ExistePessoaComNomeAsync(nome);
                    return !existe;
                })
                .WithMessage("Esta pessoa já está cadastrada no sistema.");

        RuleFor(x => x.Idade)
            .NotNull().WithMessage("A idade da pessoa é obrigatória.")
            .GreaterThan(0).WithMessage("A idade deve ser maior que zero.")
            .LessThan(120).WithMessage("Idade inválida.");

        RuleFor(x => x.Sexo)
            .NotEmpty().WithMessage("O sexo da pessoa é obrigatório.")
            .Matches(@"^[a-zA-ZáàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ\s]+$")
            .WithMessage("O sexo da pessoa não deve conter números ou caracteres especiais.");
    }

}