using GestaoDeUsuarios.Domain.Interfaces;

namespace GestaoDeUsuarios.Infrastructure.Services;

public class BcryptPasswordHasherService : IPasswordHasherService
{
    public string Hash(string senhaPlana) => BCrypt.Net.BCrypt.HashPassword(senhaPlana);
}
