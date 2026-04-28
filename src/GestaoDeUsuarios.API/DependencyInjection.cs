using System.Text.Json;
using System.Text.Json.Serialization;
using GestaoDeUsuarios.API.Converters;
using GestaoDeUsuarios.API.Filters;
using GestaoDeUsuarios.API.Middlewares;
using GestaoDeUsuarios.Application;
using GestaoDeUsuarios.Infrastructure;

namespace GestaoDeUsuarios.API;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddWebApi(this WebApplicationBuilder builder)
    {
        // Invoca a injeção de dependência da camada de Aplicação.
        builder.Services.AddApplication();

        // Invoca a injeção de dependência da camada de Infraestrutura (Persistência).
        builder.Services.AddPersistence(builder.Configuration);

        builder.Services
            // Configura os controllers e adiciona o filtro global para padronização de respostas (ApiResponse).
            .AddControllers(opts => opts.Filters.Add<ApiResponseFilter>())
            // Customiza as opções de serialização JSON para seguir o padrão snake_case e tratar datas.
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opts.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                opts.JsonSerializerOptions.Converters.Add(new NullableDateTimeConverter());
            });

        // Configuração do Swagger para documentação da API.
        builder.Services.AddSwaggerGen();
        // Necessário para o Swagger mapear os endpoints.
        builder.Services.AddEndpointsApiExplorer();

        var allowedOrigins = builder.Configuration
            .GetSection("CorsSettings:AllowedOrigins")
            .Get<string[]>() ?? [];

        // Configuração de CORS para permitir requisições do Frontend.
        builder.Services.AddCors(opts =>
        {
            opts.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        return builder;
    }
}
