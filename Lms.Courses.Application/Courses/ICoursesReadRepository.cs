using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;
using Lms.Courses.Domain.Learnings;

namespace Courses.Application.Courses;

public interface ICoursesReadRepository
{
    public Task<Course?> GetCourse(CourseId id);
    public Task<Quiz?> GetQuiz(Guid id);
    public Task<Learning?> GetLearningByCourse(CourseId courseId, Guid userId);
}