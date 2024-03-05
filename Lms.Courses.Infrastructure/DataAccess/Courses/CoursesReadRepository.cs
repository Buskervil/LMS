using Courses.Application.Courses;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;
using Lms.Courses.Domain.Learnings;
using Microsoft.EntityFrameworkCore;

namespace Lms.Courses.Infrastructure.DataAccess.Courses;

public class CoursesReadRepository : ICoursesReadRepository
{
    private readonly CoursesContext _coursesContext;

    public CoursesReadRepository(CoursesContext coursesContext)
    {
        _coursesContext = coursesContext;
    }

    public Task<Course?> GetCourse(CourseId id)
    {
        return _coursesContext
            .Courses
            .AsNoTracking()
            .AsSplitQuery()
            .Include(c => c.CourseSections)
            .ThenInclude(c => c.CourseItems)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public Task<Quiz?> GetQuiz(Guid id)
    {
        return _coursesContext.Quizes
            .AsNoTracking()
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == id);
    }

    public Task<Learning?> GetLearningByCourse(CourseId courseId, Guid userId)
    {
        return _coursesContext.Learnings
            .AsNoTracking()
            .Include(l => l.Progresses)
            .Where(l => l.CourseId == courseId && l.StudentId == userId)
            .FirstOrDefaultAsync();
    }
}