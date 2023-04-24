using CoachBoard.Application.Behaviors;
using CoachBoard.Application.Features.Users.Commands.Create;
using CoachBoard.Application.Validators.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CoachBoard.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining(typeof(CreateUserCommand)))
            .AddPipelineBehaviors()
            .AddValidators();

        return services;
    }

    private static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorPipelineBehavior<,>));

        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services
            .AddFluentValidationClientsideAdapters();
        services
            .AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

        return services;
    }
}