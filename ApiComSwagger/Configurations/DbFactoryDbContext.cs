
using ApiComSwagger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApiComSwagger.Configurations
{
    public class DbFactoryDbContext : IDesignTimeDbContextFactory<CursoDbContext>
    {
        public CursoDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<CursoDbContext>();
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ApiComSwagger;Integrated Security=True");
            CursoDbContext context = new CursoDbContext(options.Options);

            return context;
        }
    }
}
