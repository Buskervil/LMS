namespace Courses.Application.Statistics.GetCourseStatistics.Dto;

public sealed class Statistics
{
    public Guid CourseId { get; init; }
    public IReadOnlyCollection<SectionProgress> SectionProgress { get; init; }
    public IReadOnlyCollection<QuizResult> QuizResults { get; init; }
    public double TotalProgress { get; init; }
}