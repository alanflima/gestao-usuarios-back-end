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

        services.AddDbContext<GestaoDeUsuariosContext>(opts => opts.UseSqlServer(connectionString));
        services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasherService, BcryptPasswordHasherService>();

        return services;
    }
}
