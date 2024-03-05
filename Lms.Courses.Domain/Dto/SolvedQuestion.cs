namespace Lms.Courses.Domain.Courses;

public class SolvedQuestion
{
    public Guid Id { get; init; }
    public IEnumerable<SolvedAnswer> SolvedAnswers { get; init; }
}