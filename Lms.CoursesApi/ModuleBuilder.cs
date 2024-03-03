using Courses.Application.Core;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Learnings;
using Lms.Courses.Infrastructure.DataAccess;
using Lms.Courses.Infrastructure.DataAccess.Courses;
using Lms.Courses.Infrastructure.DataAccess.Learnings;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.CoursesApi;

public static class ModuleBuilder
{
    public static IServiceCollection AddCourseModule(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICoursesModule, CoursesModule>();
        serviceCollection.AddScoped<ICourseRepository, CourseRepository>();
        serviceCollection.AddScoped<ILearningRepository, LearningRepository>();
        serviceCollection.AddScoped<IUnitOfWork, CoursesContext>();

        return serviceCollection;
    }
}