using CoachBoard.API.Extensions;
//using CoachBoard.API.Middlewares;
using CoachBoard.Application;
using CoachBoard.Core.Exceptions;
using CoachBoard.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problemDetails = new ValidationProblemDetails(context.ModelState);
        throw new InputValidationException(problemDetails.Errors);
    };
});

var configuration = builder.Configuration;

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration);

builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.HandleExceptions();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();