using backend.Models;

namespace backend.Repositories;

public interface IVacinacaoRepository
{
    Task<Vacinacao?> GetByIdAsync(int id);
    Task<List<Vacinacao>> GetByPessoaAsync(int pessoaId);
    Task<Vacinacao> AddAsync(Vacinacao vacinacao);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}