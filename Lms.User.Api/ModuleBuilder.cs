using Lms.Users.Application.Core;
using Lms.Users.Domain.Sessions;
using Lms.Users.Infrastructure.DataAccess;
using Lms.Users.Infrastructure.DataAccess.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.User.Api;

public static class ModuleBuilder
{
    public static IServiceCollection AddUsersModule(this IServiceCollection serviceCollection, ConfigurationManager configurationManager)
    {
        var connectionString = configurationManager.GetConnectionString("Default")!;
        serviceCollection.AddScoped<IUsersModule, UsersModule>();
        serviceCollection.AddScoped<ISessionRepository, SessionRepository>();
        serviceCollection.AddDbContext<UsersContext>(options => options.UseNpgsql(connectionString));

        return serviceCollection;
    }
}