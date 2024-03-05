using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Lms.Courses.Infrastructure.DataAccess.Courses;

public class CourseRepository : ICourseRepository
{
    private readonly CoursesContext _coursesContext;

    public CourseRepository(CoursesContext coursesContext)
    {
        _coursesContext = coursesContext;
    }

    public async Task<Course?> GetByIdAsync(CourseId id)
    {
        var course = await _coursesContext.Courses
            .Include(c => c.CourseSections)
            .ThenInclude(s => s.CourseItems)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
        {
            return course;
        }

        var quizIds = course.Items.Where(t => t is Quiz).Select(q => q.Id).ToList();

        await _coursesContext.Quizes
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .Where(q => quizIds.Contains(q.Id))
            .LoadAsync();

        return course;
    }

    public void Add(Course course)
    {
        _coursesContext.Add(course);
    }

    public void Update(Course course)
    {
        _coursesContext.Update(course);
    }

    public void Test()
    {
        _coursesContext.Courses
            .AsSingleQuery()
            .AsNoTracking()
            .Include(c => c.CourseSections)
            .ThenInclude(s => s.CourseItems);
    }
}