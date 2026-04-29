using System.Diagnostics;
using System.Text.Json;
using FluentValidation;
using GestaoDeUsuarios.API.Models;
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
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _next(context);
            stopwatch.Stop();
        }
        catch (DomainException ex)
        {
            stopwatch.Stop();
            _logger.LogWarning(ex, "Erro de domínio: {Message}", ex.Message);
            await WriteResponse(context, StatusCodes.Status400BadRequest,
                new ApiResponse
                {
                    Sucesso = false,
                    DadosResposta = null,
                    Erros = [ex.Message],
                    TimestampResposta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    TempoDaResposta = $"{stopwatch.ElapsedMilliseconds} ms"
                });
        }
        catch (ValidationException ex)
        {
            stopwatch.Stop();
            _logger.LogWarning(ex, "Erro de validação: {Errors}", string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));
            await WriteResponse(context, StatusCodes.Status400BadRequest,
                new ApiResponse
                {
                    Sucesso = false,
                    DadosResposta = null,
                    Erros = ex.Errors.Select(e => e.ErrorMessage).ToList(),
                    TimestampResposta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    TempoDaResposta = $"{stopwatch.ElapsedMilliseconds} ms"
                });
        }
        catch (KeyNotFoundException ex)
        {
            stopwatch.Stop();
            _logger.LogWarning(ex, "Recurso não encontrado: {Message}", ex.Message);
            await WriteResponse(context, StatusCodes.Status404NotFound,
                new ApiResponse
                {
                    Sucesso = false,
                    DadosResposta = null,
                    Erros = [ex.Message],
                    TimestampResposta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    TempoDaResposta = $"{stopwatch.ElapsedMilliseconds} ms"
                });
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
            await WriteResponse(context, StatusCodes.Status500InternalServerError,
                new ApiResponse
                {
                    Sucesso = false,
                    DadosResposta = null,
                    Erros = ["Erro interno do servidor."],
                    TimestampResposta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    TempoDaResposta = $"{stopwatch.ElapsedMilliseconds} ms"
                });
        }
    }

    private static async Task WriteResponse(HttpContext context, int statusCode, ApiResponse body)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(body,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower }));
    }
}
