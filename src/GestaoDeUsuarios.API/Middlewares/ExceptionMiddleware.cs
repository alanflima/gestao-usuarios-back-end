using System.Text.Json;
using FluentValidation;
using GestaoDeUsuarios.Domain.Exceptions;

namespace GestaoDeUsuarios.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Erro de domínio: {Message}", ex.Message);
            await WriteResponse(context, StatusCodes.Status400BadRequest,
                new { error = ex.Message });
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "Erro de validação: {Errors}", string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));
            var errors = ex.Errors.Select(e => e.ErrorMessage);
            await WriteResponse(context, StatusCodes.Status400BadRequest,
                new { errors });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Recurso não encontrado: {Message}", ex.Message);
            await WriteResponse(context, StatusCodes.Status404NotFound,
                new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
            await WriteResponse(context, StatusCodes.Status500InternalServerError,
                new { error = "Erro interno do servidor." });
        }
    }

    private static async Task WriteResponse(HttpContext context, int statusCode, object body)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(body,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower }));
    }
}
