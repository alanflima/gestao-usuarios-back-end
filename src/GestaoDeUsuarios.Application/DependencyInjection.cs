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

        // Registro do AutoMapper para realizar mapeamentos entre Entidades e DTOs.
        // Procura por perfis de mapeamento (Profile) neste assembly.
        services.AddAutoMapper(cfg => cfg.AddMaps(assembly));

        // Registro de todos os validadores do FluentValidation presentes no assembly.
        // Utilizado para validar os DTOs de entrada nas requisições.
        services.AddValidatorsFromAssembly(assembly);

        // Registro do Serviço de Aplicação de Usuário (Scoped).
        // Contém a lógica de negócio e orquestração da funcionalidade de gestão de usuários.
        services.AddScoped<IUsuarioAppService, UsuarioAppService>();

        return services;
    }
}
