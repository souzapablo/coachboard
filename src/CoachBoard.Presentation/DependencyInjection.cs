using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoachBoard.Presentation;
public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(Assembly.GetExecutingAssembly());

        return services;
    }
}
