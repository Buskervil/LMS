using Courses.Application.Core;
using Lms.Courses.Domain.Course;
using Lms.Courses.Infrastructure.DataAccess.Courses;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.CoursesApi;

public static class ModuleBuilder
{
    public static IServiceCollection AddCourseModule(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICoursesModule, CoursesModule>();
        serviceCollection.AddScoped<ICourseRepository, CourseRepository>();

        return serviceCollection;
    }
}