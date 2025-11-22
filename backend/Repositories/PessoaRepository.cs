using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _context;
    
    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pessoa?> GetByIdAsync(int id)
    {
        return await _context.Pessoa
            .Include(p => p.Vacinacoes)
            .ThenInclude(v => v.Vacina)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Pessoa>> GetAllAsync()
    {
        return await _context.Pessoa.ToListAsync();
    }

    public async Task<Pessoa> AddAsync(Pessoa pessoa)
    {
        _context.Pessoa.Add(pessoa);
        await _context.SaveChangesAsync();
        return pessoa;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pessoa = await _context.Pessoa.FindAsync(id);
        if (pessoa == null)
            return false;

        _context.Pessoa.Remove(pessoa);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}