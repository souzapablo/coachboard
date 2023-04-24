using System.Text.Json;
using CoachBoard.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CoachBoard.API.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(
        ILogger<GlobalExceptionHandlingMiddleware> logger) =>
        _logger = logger;


    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BaseException e)
        {
            _logger.LogError("{e}", e);

            context.Response.StatusCode =
                (int)e.HttpStatusCode;

            ProblemDetails problem = new()
            {
                Status = (int)e.HttpStatusCode,
                Type = e.HttpStatusCode.ToString(),
                Title = e.GetType().Name,
                Detail = e.Message
            };

            var json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}