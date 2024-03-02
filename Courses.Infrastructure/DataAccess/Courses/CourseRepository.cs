using Courses.Domain.Course;
using Courses.Domain.Course.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Courses.Infrastructure.DataAccess.Courses;

public class CourseRepository : ICourseRepository
{
    private readonly CoursesContext _coursesContext;

    public CourseRepository(CoursesContext coursesContext)
    {
        _coursesContext = coursesContext;
    }

    public Task<Course?> GetById(CourseId id)
    {
        return _coursesContext.Courses
            .Include(c => c.CourseSections)
            .ThenInclude(s => s.CourseItems)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task Add(Course course)
    {
        await _coursesContext.AddAsync(course);
    }

    public void Update(Course course)
    {
        _coursesContext.Update(course);
    }
}