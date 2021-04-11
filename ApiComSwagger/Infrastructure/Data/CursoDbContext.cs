using ApiComSwagger.Business.Entities;
using ApiComSwagger.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace ApiComSwagger.Infrastructure.Data
{
    /// <summary>
    /// Configuração do EF para Curso
    /// </summary>
    public class CursoDbContext : DbContext
    {
        public CursoDbContext(DbContextOptions<CursoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration (new CursoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
