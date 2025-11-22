using Microsoft.EntityFrameworkCore;
using backend.Models;   

namespace backend.Data   
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pessoa> Pessoa { get; set; } = null!;
        public DbSet<Vacina> Vacinas { get; set; } = null!;
        public DbSet<Vacinacao> Vacinacoes { get; set; } = null!;
    }
}
