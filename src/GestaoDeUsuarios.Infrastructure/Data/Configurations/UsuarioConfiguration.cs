using GestaoDeUsuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoDeUsuarios.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("IX_Usuarios_Email");

        builder.Property(u => u.SenhaHash)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(u => u.Ativo)
            .IsRequired();

        builder.Property(u => u.CriadoEm)
            .IsRequired()
            .HasColumnType("datetime2(0)");

        builder.Property(u => u.AtualizadoEm)
            .IsRequired(false)
            .HasColumnType("datetime2(0)");

        builder.Property(u => u.Cargo)
            .IsRequired(false)
            .HasMaxLength(100);
    }
}
