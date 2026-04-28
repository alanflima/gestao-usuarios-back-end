using System.Diagnostics;
using GestaoDeUsuarios.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GestaoDeUsuarios.API.Filters;

public class ApiResponseFilter : IActionFilter
{
    private Stopwatch _stopwatch = new();

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();

        if (context.Result is ObjectResult objectResult)
        {
            objectResult.Value = new ApiResponse
            {
                DadosResposta = objectResult.Value,
                TimestampResposta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                TempoDaResposta = $"{_stopwatch.ElapsedMilliseconds} ms"
            };
        }
    }
}
