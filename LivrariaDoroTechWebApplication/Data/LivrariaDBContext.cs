using LivrariaDoroTechWebApplication.Data.Map;
using LivrariaDoroTechWebApplication.Models;

using Microsoft.EntityFrameworkCore;

namespace LivrariaDoroTechWebApplication.Data
{
    public class LivrariaDBContext : DbContext
    {
        public LivrariaDBContext(DbContextOptions<LivrariaDBContext> options) 
            : base(options)
        {
        }
        public DbSet<LivroModel> Livros { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LivroMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
