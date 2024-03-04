using Courses.Application.Courses;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;
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
}