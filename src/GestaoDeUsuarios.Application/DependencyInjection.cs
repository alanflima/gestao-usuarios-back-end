using FluentValidation;
using GestaoDeUsuarios.Application.Usuarios;
using GestaoDeUsuarios.Application.Usuarios.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoDeUsuarios.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddAutoMapper(cfg => cfg.AddMaps(assembly));
        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped<IUsuarioAppService, UsuarioAppService>();

        return services;
    }
}
