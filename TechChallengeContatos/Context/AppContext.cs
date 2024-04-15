using Microsoft.EntityFrameworkCore;
using TechChallengeContatos.Entities;

namespace TechChallengeContatos.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Contato> Contatos{ get; set; }
        public DbSet<Caderneta> Cadernetas{ get; set; }
    }
}
