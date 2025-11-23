using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PessoaController : ControllerBase
{
    private readonly PessoaService _pessoaService;
    private readonly VacinacaoService _vacinacaoService;

    public PessoaController(PessoaService pessoaService, VacinacaoService vacinacaoService)
    {
        _pessoaService = pessoaService;
        _vacinacaoService = vacinacaoService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarPessoa(PessoaCreateDTO dto)
    {
        var pessoa = await _pessoaService.CriarPessoaAsync(dto);
        return Ok(pessoa);
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