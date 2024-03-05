namespace Lms.Courses.Domain.Courses;

public class SolvedAnswer
{
    public Guid Id { get; init; }
    public Guid QuestionId { get; init; }
    public bool IsSelected { get; init; }
}