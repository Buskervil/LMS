namespace Lms.Courses.Domain.Dto;

public class SolvedAnswer
{
    public Guid Id { get; init; }
    public Guid QuestionId { get; init; }
    public bool IsSelected { get; init; }
}