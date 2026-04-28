using GestaoDeUsuarios.Domain.Interfaces;
using GestaoDeUsuarios.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestaoDeUsuarios.Infrastructure.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly GestaoDeUsuariosContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(GestaoDeUsuariosContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Remove(T entity) => _dbSet.Remove(entity);
}
