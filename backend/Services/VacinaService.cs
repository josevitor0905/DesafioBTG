using backend.DTOs;
using backend.Models;
using backend.Repositories;

namespace backend.Services;

public class VacinaService
{
    private readonly IVacinaRepository _vacinaRepo;

    public VacinaService(IVacinaRepository vacinaRepo)
    {
        _vacinaRepo = vacinaRepo;
    }

    public async Task<Vacina> CriarVacinaAsync(VacinaCreateDTO dto)
    {
        var vacina = new Vacina
        {
            Nome = dto.Nome
        };

        return await _vacinaRepo.AddAsync(vacina);
    }

    public async Task<bool> RemoverVacinaAsync(int id)
    {
        return await _vacinaRepo.DeleteAsync(id);
    }

    public async Task<List<Vacina>> ListarAsync()
    {
        return await _vacinaRepo.GetAllAsync();
    }
}