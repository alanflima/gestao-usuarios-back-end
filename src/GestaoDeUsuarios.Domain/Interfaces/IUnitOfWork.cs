namespace GestaoDeUsuarios.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SaveAsync();
}
