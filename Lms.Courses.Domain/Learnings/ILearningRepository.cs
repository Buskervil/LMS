namespace Lms.Courses.Domain.Learnings;

public interface ILearningRepository
{
    Task<Learning?> GetByIdAsync(Guid id);
    void Add(Learning learning);
    void Update(Learning learning);
}