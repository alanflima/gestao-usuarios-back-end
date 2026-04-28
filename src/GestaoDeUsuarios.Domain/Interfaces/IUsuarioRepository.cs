using GestaoDeUsuarios.Domain.Entities;

namespace GestaoDeUsuarios.Domain.Interfaces;

public interface IUsuarioRepository : IRepositoryBase<Usuario>
{
    Task<bool> ExisteEmailAsync(string email, Guid? ignorarId = null);
    Task<IEnumerable<Usuario>> GetAllAtivosAsync();
    Task<IEnumerable<Usuario>> GetAllInativosAsync();
    Task<IEnumerable<Usuario>> GetByNameAsync(string nome);
    Task<IEnumerable<Usuario>> GetByEmailAsync(string email);
}
