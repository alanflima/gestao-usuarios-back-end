using System.Text.Json;
using System.Text.Json.Serialization;
using GestaoDeUsuarios.API.Filters;
using GestaoDeUsuarios.API.Middlewares;
using GestaoDeUsuarios.Application;
using GestaoDeUsuarios.Infrastructure;

namespace GestaoDeUsuarios.API;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddWebApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplication();
        builder.Services.AddPersistence(builder.Configuration);

        builder.Services
            .AddControllers(opts => opts.Filters.Add<ApiResponseFilter>())
            .AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                opts.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                opts.JsonSerializerOptions.Converters.Add(new NullableDateTimeConverter());
            });

        builder.Services.AddSwaggerGen();
        builder.Services.AddEndpointsApiExplorer();

        var allowedOrigins = builder.Configuration
            .GetSection("CorsSettings:AllowedOrigins")
            .Get<string[]>() ?? [];

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

public class DateTimeConverter : JsonConverter<DateTime>
{
    private const string Format = "dd/MM/yyyy HH:mm:ss";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => DateTime.ParseExact(reader.GetString()!, Format, null);

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(Format));
}

public class NullableDateTimeConverter : JsonConverter<DateTime?>
{
    private const string Format = "dd/MM/yyyy HH:mm:ss";

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var s = reader.GetString();
        return s is null ? null : DateTime.ParseExact(s, Format, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString(Format));
        else
            writer.WriteNullValue();
    }
}
