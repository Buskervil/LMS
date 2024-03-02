using Courses.Application.Core;
using Lms.Core.Application.Sessions;
using Lms.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Core.Api;

public static class ModuleBuilder
{
    public static IServiceCollection AddCoreModule(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ISessionProvider, SessionProvider>();
        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ICoursesModule).Assembly);
        });


        return serviceCollection;
    }
}