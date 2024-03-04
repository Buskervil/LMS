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

    public Task<Course?> GetByIdAsync(CourseId id)
    {
        return _coursesContext.Courses
            .Include(c => c.CourseSections)
            .ThenInclude(s => s.CourseItems)
            .FirstOrDefaultAsync(c => c.Id == id);
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