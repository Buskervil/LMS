namespace Lms.Courses.Domain.Dto;

public class SolvedQuestion
{
    public Guid Id { get; init; }
    public IEnumerable<SolvedAnswer> SolvedAnswers { get; init; }
}