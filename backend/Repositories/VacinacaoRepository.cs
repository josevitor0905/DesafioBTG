using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class VacinacaoRepository : IVacinacaoRepository
{
    private readonly AppDbContext _context;

    public VacinacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Vacinacao?> GetByIdAsync(int id)
    {
        return await _context.Vacinacoes
            .Include(v => v.Vacina)
            .Include(v => v.Pessoa)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<List<Vacinacao>> GetByPessoaAsync(int pessoaId)
    {
        return await _context.Vacinacoes
            .Where(v => v.PessoaId == pessoaId)
            .Include(Vacina => Vacina.Vacina)
            .ToListAsync();
    }

    public async Task<Vacinacao> AddAsync(Vacinacao vacinacao)
    {
        _context.Vacinacoes.Add(vacinacao);
        await _context.SaveChangesAsync();
        return vacinacao;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var vacinacao = await _context.Vacinacoes.FindAsync(id);
        
        if (vacinacao == null)
        {
            return false;
        }

        _context.Vacinacoes.Remove(vacinacao);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}