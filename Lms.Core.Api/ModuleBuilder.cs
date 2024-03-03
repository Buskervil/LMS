using Courses.Application.Core;
using Lms.Core.Application.Sessions;
using Lms.Core.Domain.Primitives;
using Lms.Core.Infrastructure;
using Lms.Courses.Infrastructure.Processing;
using Microsoft.AspNetCore.Mvc;
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
            cfg.AddOpenBehavior(typeof(UnitOfWorkCommandPipelineBehavior<,>));
        });


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