using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;

namespace Courses.Application.Courses;

public interface ICoursesReadRepository
{
    public Task<Course?> GetCourse(CourseId id);
}