using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VacinaController : ControllerBase
{
    private readonly VacinaService _vacinaService;

    public VacinaController(VacinaService vacinaService)
    {
        _vacinaService = vacinaService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarVacina(VacinaCreateDTO dto)
    {
        var vacina = await _vacinaService.CriarVacinaAsync(dto);
        return Ok(vacina);
    }

    [HttpGet]
    public async Task<IActionResult> ListarVacinas()
    {
        var vacinas = await _vacinaService.ListarAsync();
        return Ok(vacinas);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var removido = await _vacinaService.RemoverVacinaAsync(id);
        
        if (!removido)
        {
            return NotFound("Vacina não encontrada.");
        }

        return NoContent();
    }
}