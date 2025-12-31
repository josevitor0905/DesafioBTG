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
        return await _context.Pessoas
            .Include(p => p.Vacinacoes)
            .ThenInclude(v => v.Vacina)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Pessoa>> GetAllAsync()
    {
        return await _context.Pessoas.ToListAsync();
    }

    public async Task<Pessoa> AddAsync(Pessoa pessoa)
    {
        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();
        return pessoa;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if (pessoa == null)
            return false;

        _context.Pessoas.Remove(pessoa);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Pessoa?> GetByNomeAsync(string nome)
    {
        return await _context.Pessoas
            .Include(p => p.Vacinacoes)
            .ThenInclude(v => v.Vacina)
            .FirstOrDefaultAsync(p => p.Nome == nome);
    }
}