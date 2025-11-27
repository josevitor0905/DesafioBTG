using backend.Models;
namespace backend.Repositories;

public interface IPessoaRepository
{
    Task<Pessoa?> GetByIdAsync(int id);
    Task<List<Pessoa>> GetAllAsync();
    Task<Pessoa> AddAsync(Pessoa pessoa);
    Task<bool> DeleteAsync(int id);
    Task SaveChangesAsync();
}