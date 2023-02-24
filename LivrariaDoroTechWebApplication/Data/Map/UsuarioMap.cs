using LivrariaDoroTechWebApplication.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivrariaDoroTechWebApplication.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Login).IsRequired();
            builder.Property(a => a.Senha).IsRequired();
        }
    }
}
