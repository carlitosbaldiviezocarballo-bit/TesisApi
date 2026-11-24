using Microsoft.AspNetCore.Mvc;
using TesisApi.Domain.Exceptions;

namespace TesisApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await _next(ctx);
        }
        catch (Exception ex)
        {
            var (status, title) = ex switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "No encontrado"),
                BusinessRuleException => (StatusCodes.Status409Conflict, "Regla de negocio"),
                ValidationAppException => (StatusCodes.Status400BadRequest, "Dato inválido"),
                _ => (StatusCodes.Status500InternalServerError, "Error interno")
            };

            if (status == 500) _logger.LogError(ex, "Error no controlado");

            ctx.Response.StatusCode = status;
            await ctx.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = status == 500 ? "Ocurrió un error inesperado." : ex.Message
            });
        }
    }
}
