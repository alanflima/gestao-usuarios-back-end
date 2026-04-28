using GestaoDeUsuarios.Domain.Interfaces;
using GestaoDeUsuarios.Infrastructure.Data;
using GestaoDeUsuarios.Infrastructure.Repositories;
using GestaoDeUsuarios.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoDeUsuarios.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Configura o DbContext com SQL Server. Scoped por padrão.
        services.AddDbContext<GestaoDeUsuariosContext>(opts => opts.UseSqlServer(connectionString));

        // Registro do Repositório Genérico para operações CRUD básicas.
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

        // Registro do Repositório específico de Usuário para consultas personalizadas.
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        // Registro do Unit of Work para garantir a atomicidade das transações.
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Serviço de hashing de senhas utilizando BCrypt.
        services.AddScoped<IPasswordHasherService, BcryptPasswordHasherService>();

        return services;
    }
}
