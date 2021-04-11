using ApiComSwagger.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiComSwagger.Infrastructure.Data.Mappings
{
    public class CursoMapping : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("TB_CURSO");
            builder.HasKey(p => p.Codigo);
            builder.Property(p => p.Codigo).ValueGeneratedOnAdd();
            builder.Property(prop => prop.Descricao);
            builder.Property(prop => prop.Nome);
            builder.HasOne(prop => prop.Usuario).WithMany().HasForeignKey(fk => fk.CodigoUsuario);
        }
    }
}
