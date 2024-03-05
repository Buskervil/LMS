namespace Courses.Application.Courses.GetItem.Dto;

public sealed class Answer
{
    public Guid Id { get; init; }
    public Guid QuestionId { get; init; }
    public string Content { get; init; }
}