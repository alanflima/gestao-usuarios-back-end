using GestaoDeUsuarios.Domain.Entities;
using GestaoDeUsuarios.Domain.Interfaces;
using GestaoDeUsuarios.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeUsuarios.Infrastructure.Repositories;

public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(GestaoDeUsuariosContext context) : base(context) { }

    public async Task<bool> ExisteEmailAsync(string email, Guid? ignorarId = null)
    {
        var query = _dbSet.Where(u => u.Email == email);

        if (ignorarId.HasValue)
            query = query.Where(u => u.Id != ignorarId.Value);

        return await query.AnyAsync();
    }

    public async Task<IEnumerable<Usuario>> GetAllAtivosAsync()
        => await _dbSet.Where(u => u.Ativo).ToListAsync();

    public async Task<IEnumerable<Usuario>> GetAllInativosAsync()
        => await _dbSet.Where(u => !u.Ativo).ToListAsync();

    public async Task<IEnumerable<Usuario>> GetByNameAsync(string nome)
        => await _dbSet.Where(u => u.Nome.Contains(nome)).ToListAsync();

    public async Task<IEnumerable<Usuario>> GetByEmailAsync(string email)
        => await _dbSet.Where(u => u.Email.Contains(email)).ToListAsync();
}
