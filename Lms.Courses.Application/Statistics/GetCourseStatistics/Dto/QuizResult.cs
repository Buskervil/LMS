namespace Courses.Application.Statistics.GetCourseStatistics.Dto;

public class QuizResult
{
    public Guid QuizId { get; init; }
    public string Name { get; init; }
    public int ResultPercent { get; init; }
}