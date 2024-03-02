using Courses.Domain.Course.ValueObjects;

namespace Courses.Domain.Course;

public interface ICourseRepository
{
    Task<Course?> GetById(CourseId id);
    Task Add(Course course);
    void Update(Course course);
}