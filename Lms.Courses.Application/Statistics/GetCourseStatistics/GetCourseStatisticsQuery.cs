using Courses.Application.Core;

namespace Courses.Application.Statistics.GetCourseStatistics;

public sealed class GetCourseStatisticsQuery : IQuery<Dto.Statistics>
{
    public Guid CourseId { get; init; }
}