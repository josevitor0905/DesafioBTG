using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class VacinaRepository : IVacinaRepository
{
    private readonly AppDbContext _context;

    public VacinaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Vacina?> GetByIdAsync(int id)
    {
        return await _context.Vacinas.FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<List<Vacina>> GetAllAsync()
    {
        return await _context.Vacinas.ToListAsync();
    }

    public async Task<Vacina> AddAsync(Vacina vacina)
    {
        _context.Vacinas.Add(vacina);
        await _context.SaveChangesAsync();
        return vacina;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}