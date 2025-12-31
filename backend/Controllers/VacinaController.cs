using backend.DTOs;
using backend.Services;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VacinaController : ControllerBase
{
    private readonly VacinaService _vacinaService;
    private readonly IValidator<VacinaCreateDTO> _validator;

    public VacinaController(VacinaService vacinaService, IValidator<VacinaCreateDTO> validator)
    {
        _vacinaService = vacinaService;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CriarVacina(VacinaCreateDTO dto)
    {
        var result = await _validator.ValidateAsync(dto);
        if (!result.IsValid)
        {
            return BadRequest(result.ToDictionary());
        }
        else
        {
            var vacina = await _vacinaService.CriarVacinaAsync(dto);
            return Ok(vacina);
        }  
    }

    [HttpGet]
    public async Task<IActionResult> ListarVacinas()
    {
        var vacinas = await _vacinaService.ListarAsync();
        return Ok(vacinas);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverVacina(int id)
    {
        var removido = await _vacinaService.RemoverVacinaAsync(id);
        
        if (!removido)
        {
            return NotFound("Vacina não encontrada.");
        }

        return NoContent();
    }
}