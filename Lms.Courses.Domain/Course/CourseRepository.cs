using Lms.Courses.Domain.Course.ValueObjects;

namespace Lms.Courses.Domain.Course;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(CourseId id);
    Task AddAsync(Course course);
    void Update(Course course);
}