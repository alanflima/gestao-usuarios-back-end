using GestaoDeUsuarios.Domain.Interfaces;
using GestaoDeUsuarios.Infrastructure.Data;

namespace GestaoDeUsuarios.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly GestaoDeUsuariosContext _context;

    public UnitOfWork(GestaoDeUsuariosContext context)
    {
        _context = context;
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
