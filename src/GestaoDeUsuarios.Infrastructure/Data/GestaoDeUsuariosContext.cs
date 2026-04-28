using GestaoDeUsuarios.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeUsuarios.Infrastructure.Data;

public class GestaoDeUsuariosContext : DbContext
{
    public GestaoDeUsuariosContext(DbContextOptions<GestaoDeUsuariosContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoDeUsuariosContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
