namespace GestaoDeUsuarios.Domain.Interfaces;

public interface IRepositoryBase<T>
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}
