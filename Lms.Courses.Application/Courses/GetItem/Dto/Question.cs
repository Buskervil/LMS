namespace Courses.Application.Courses.GetItem.Dto;

public sealed class Question
{
    public IEnumerable<Answer> Answers { get; init; }
    public Guid Id { get; init; }
    public Guid QuizId { get; init; }
    public string Content { get; init; }
    public bool AllowMultipleAnswers { get; init; }
}