using Courses.Application.Core;
using Lms.Core.Application;
using Lms.Core.Application.Connections;
using Lms.Core.Application.Sessions;
using Lms.Core.Domain.Primitives;
using Lms.Core.Infrastructure;
using Lms.Courses.Infrastructure.Processing;
using Lms.Users.Application.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Core.Api;

public static class ModuleBuilder
{
    public static IServiceCollection AddCoreModule(this IServiceCollection serviceCollection, ConfigurationManager configurationManager)
    {
        var connectionString = configurationManager.GetConnectionString("Default")!;
        serviceCollection.AddScoped<ISessionProvider, SessionProvider>();
        serviceCollection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ICoursesModule).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(IUsersModule).Assembly);
            cfg.AddOpenBehavior(typeof(UnitOfWorkCommandPipelineBehavior<,>));
        });
        serviceCollection.AddScoped<AuthorizeFilter>();
        serviceCollection.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
        serviceCollection.AddSingleton<IConnectionStringProvider>(new ConnectionStringProvider(connectionString));

        return serviceCollection;
    }
}

public class BaseController : Controller
{
    protected IActionResult CreateErrorResponse(ApiError apiError)
    {
        return StatusCode((int)apiError.Code, new { Error = apiError.Message });
    }
}