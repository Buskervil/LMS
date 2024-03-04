using Courses.Application.Core;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Learnings;
using Lms.Courses.Infrastructure.DataAccess;
using Lms.Courses.Infrastructure.DataAccess.Courses;
using Lms.Courses.Infrastructure.DataAccess.Learnings;
using Lms.Users.Domain.Employees;
using Lms.Users.Infrastructure.DataAccess.AuthenticationLogins;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.CoursesApi;

public static class ModuleBuilder
{
    public static IServiceCollection AddCourseModule(this IServiceCollection serviceCollection, ConfigurationManager configurationManager)
    {
        var connectionString = configurationManager.GetConnectionString("Default")!;
        serviceCollection.AddScoped<ICoursesModule, CoursesModule>();
        serviceCollection.AddScoped<ICourseRepository, CourseRepository>();
        serviceCollection.AddScoped<ILearningRepository, LearningRepository>();
        serviceCollection.AddScoped<IAuthenticationLoginRepository, AuthenticationLoginsRepository>();
        serviceCollection.AddScoped<IUnitOfWork, CoursesContext>();
        serviceCollection.AddDbContext<CoursesContext>(options => options.UseNpgsql(connectionString));
        
        return serviceCollection;
    }
}