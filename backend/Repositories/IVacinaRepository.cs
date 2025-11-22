using backend.Models;
namespace backend.Repositories;

public interface IVacinaRepository
{
    Task<Vacina?> GetByIdAsync(int id);
    Task<List<Vacina>> GetAllAsync();
    Task<Vacina> AddAsync(Vacina vacina);
    Task SaveChangesAsync();
}