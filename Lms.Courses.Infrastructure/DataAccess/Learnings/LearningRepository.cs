using Lms.Courses.Domain.Learnings;
using Microsoft.EntityFrameworkCore;

namespace Lms.Courses.Infrastructure.DataAccess.Learnings;

public class LearningRepository : ILearningRepository
{
    private readonly CoursesContext _coursesContext;

    public LearningRepository(CoursesContext coursesContext)
    {
        _coursesContext = coursesContext;
    }

    public Task<Learning?> GetByIdAsync(Guid id)
    {
        return _coursesContext.Learnings
            .Include(l => l.Progresses)
            .FirstOrDefaultAsync(l => l.Id == id);
    }

    public void Add(Learning learning)
    {
        _coursesContext.Add(learning);
    }

    public void Update(Learning learning)
    {
        _coursesContext.Update(learning);
    }
}