using CoachBoard.Application.Features.Users.Commands.Create;
using Microsoft.Extensions.DependencyInjection;

namespace CoachBoard.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblyContaining(typeof(CreateUserCommand)));

        return services;
    }
}