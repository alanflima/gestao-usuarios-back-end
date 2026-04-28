namespace GestaoDeUsuarios.Domain.Interfaces;

public interface IPasswordHasherService
{
    string Hash(string senhaPlana);
}
