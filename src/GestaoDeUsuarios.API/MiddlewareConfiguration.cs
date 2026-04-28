using GestaoDeUsuarios.API.Middlewares;

namespace GestaoDeUsuarios.API;

public static class MiddlewareConfiguration
{
    public static WebApplication UseWebApi(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors("AllowFrontend");

        app.UseRouting();

        app.MapControllers();

        return app;
    }
}
