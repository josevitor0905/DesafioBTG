using Microsoft.EntityFrameworkCore;
using backend.Models;   

namespace backend.Data;  

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; } = null!;
        public DbSet<Vacina> Vacinas { get; set; } = null!;
        public DbSet<Vacinacao> Vacinacoes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vacina>().HasData(
            new Vacina { Id = 1, Nome = "BCG" },
            new Vacina { Id = 2, Nome = "Hepatite B" },
            new Vacina { Id = 3, Nome = "Pentavalente" },
            new Vacina { Id = 4, Nome = "VIP (Poliomielite Inativada)" },
            new Vacina { Id = 5, Nome = "VOP (Poliomielite Oral)" }
            );

        modelBuilder.Entity<Pessoa>().HasData(
            new Pessoa { Id = 1, Nome = "João Pessoa"},
            new Pessoa { Id = 2, Nome = "Maria Fumaça"}
            );
        }
    }
