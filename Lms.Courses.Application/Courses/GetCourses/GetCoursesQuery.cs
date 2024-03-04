using Courses.Application.Core;

namespace Courses.Application.Courses.GetCourses;

public sealed class GetCoursesQuery : IQuery<IReadOnlyCollection<CourseData>>
{
}