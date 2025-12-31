using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PessoaController : ControllerBase
{
    private readonly PessoaService _pessoaService;
    private readonly VacinacaoService _vacinacaoService;
    private readonly IValidator<PessoaCreateDTO> _validator;
    
    public PessoaController(PessoaService pessoaService, VacinacaoService vacinacaoService, IValidator<PessoaCreateDTO> validator)
    {
        _pessoaService = pessoaService;
        _vacinacaoService = vacinacaoService;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CriarPessoa(PessoaCreateDTO dto)
    {
        var result = await _validator.ValidateAsync(dto);

        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }
        else
        {
            var pessoa = await _pessoaService.CriarPessoaAsync(dto);
            return Ok(pessoa);
        }

    }

    [HttpGet]
    public async Task<IActionResult> ListarPessoas()
    {
        var pessoas = await _pessoaService.ListarAsync();
        return Ok(pessoas);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverPessoa(int id)
    {
        var removido = await _pessoaService.RemoverPessoaAsync(id);
        
        if (!removido)
        {
            return NotFound("Pessoa não encontrada.");
        }

        return NoContent();
    }

    [HttpGet("{id}/cartao")]
    public async Task<IActionResult> ObterCartao(int id)
    {
        var vacs = await _vacinacaoService.ObterPorPessoaAsync(id);
        return Ok(vacs);
    }
}