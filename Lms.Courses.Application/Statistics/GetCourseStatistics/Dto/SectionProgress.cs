namespace Courses.Application.Statistics.GetCourseStatistics.Dto;

public class SectionProgress
{
    public Guid SectionId { get; init; }
    public string SectionName { get; init; }
    public double ProgressPercent { get; init; }
}