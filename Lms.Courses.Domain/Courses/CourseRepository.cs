using Lms.Courses.Domain.Courses.ValueObjects;

namespace Lms.Courses.Domain.Courses;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(CourseId id);
    void Add(Course course);
    void Update(Course course);
}