using System.Diagnostics;
using GestaoDeUsuarios.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GestaoDeUsuarios.API.Filters;

public class ApiResponseFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();
        var executedContext = await next();
        stopwatch.Stop();

        if (executedContext.Exception is not null)
            return;

        if (executedContext.Result is ObjectResult objectResult)
        {
            objectResult.Value = new ApiResponse
            {
                Sucesso = true,
                DadosResposta = objectResult.Value,
                Erros = [],
                TimestampResposta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                TempoDaResposta = $"{stopwatch.ElapsedMilliseconds} ms"
            };
        }
    }
}
