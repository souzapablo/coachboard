using System.Diagnostics;
using System.Text.Json;
using CoachBoard.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace CoachBoard.API.Extensions;

public static class ExceptionExtensions
{
    public static IApplicationBuilder HandleExceptions(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = errorFeature?.Error;

                var problemDetails = new ProblemDetails
                {
                    Type = exception?.GetType().Name,
                    Title = "An unexpected error occurred!",
                    Detail = exception?.Message,
                    Instance = errorFeature switch
                    {
                        ExceptionHandlerFeature e => e.Path,
                        _ => "unknown"
                    },
                    Status = StatusCodes.Status400BadRequest,
                    Extensions =
                    {
                        ["trace"] = Activity.Current?.Id ?? context.TraceIdentifier
                    }
                };

                switch (exception)
                {
                    case InputValidationException validationException:
                        problemDetails.Status = StatusCodes.Status400BadRequest;
                        problemDetails.Title = "One or more validation errors occurred";
                        problemDetails.Detail =
                            "The request contains invalid parameters. More information can be found in the errors.";
                        problemDetails.Extensions["errors"] = validationException.Errors;
                        break;
                    case BaseException baseException:
                        problemDetails.Status = (int)baseException.HttpStatusCode;
                        problemDetails.Title = baseException.GetType().Name + " AQUI";
                        problemDetails.Detail = baseException.Message;
                        problemDetails.Type = baseException.HttpStatusCode.ToString();
                        break;
                }

                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = problemDetails.Status.Value;
                context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
                {
                    NoCache = true,
                };
                await JsonSerializer.SerializeAsync(context.Response.Body, problemDetails);
            });
        });
        return app;
    }
}