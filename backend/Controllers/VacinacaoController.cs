using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VacinacaoController : ControllerBase
{
    private readonly VacinacaoService _vacinacaoService;

    public VacinacaoController(VacinacaoService vacinacaoService)
    {
        _vacinacaoService = vacinacaoService;
    }

    [HttpPost]
    public async Task<IActionResult> Registrar(VacinacaoCreateDTO dto)
    {
        try
        {
            var vac = await _vacinacaoService.RegistrarAsync(dto);
            
            if (vac == null)
            {
                return BadRequest("Pessoa ou vacina não encontrada.");
            }

            return Ok(vac);
        }
        catch (Exception ex)
        {
            return BadRequest(new {erro = ex.Message});
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var removido = await _vacinacaoService.RemoverAsync(id);
        
        if (!removido)
        {
            return NotFound("Vacinação não encontrada.");
        }

        return NoContent();
    }
}