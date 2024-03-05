using Courses.Application.Core;
using Courses.Application.Courses;
using Courses.Application.Statistics.GetCourseStatistics.Dto;
using Lms.Core.Application.Sessions;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;

namespace Courses.Application.Statistics.GetCourseStatistics;

// ReSharper disable once UnusedType.Global
internal sealed class GetCourseStatisticsQueryHandler : IQueryHandler<GetCourseStatisticsQuery, Dto.Statistics>
{
    private readonly ICoursesReadRepository _coursesReadRepository;
    private readonly ISessionProvider _sessionProvider;

    public GetCourseStatisticsQueryHandler(ICoursesReadRepository coursesReadRepository, ISessionProvider sessionProvider)
    {
        _coursesReadRepository = coursesReadRepository;
        _sessionProvider = sessionProvider;
    }

    public async Task<Result<Dto.Statistics>> Handle(GetCourseStatisticsQuery request, CancellationToken cancellationToken)
    {
        var course = await _coursesReadRepository.GetCourse(CourseId.Create(request.CourseId));

        var sections = course.CourseSections;
        var learning = await _coursesReadRepository.GetLearningByCourse(course.Id, _sessionProvider.UserId);

        var quizResults = new List<QuizResult>();
        foreach (var quiz in course.Items.Where(t => t is Quiz))
        {
            var progress = learning?.Progresses.Where(p => p.CourseItemId == quiz.Id).MaxBy(p => p.CommittedAt);

            if (progress != null)
            {
                quizResults.Add(new QuizResult()
                {
                    Name = quiz.Name,
                    QuizId = quiz.Id,
                    ResultPercent = progress.ScoreInPercent
                });
            }
        }

        return new Dto.Statistics()
        {
            CourseId = course.Id,
            SectionProgress = sections
                .Select(s => new SectionProgress()
                {
                    SectionId = s.Id,
                    SectionName = s.Name,
                    ProgressPercent = s.CourseItems.Count == 0 ? 0 :
                        (double)learning.Progresses
                            .Where(p => s.CourseItems.Any(i => i.Id == p.CourseItemId))
                            .DistinctBy(p => p.CourseItemId)
                            .Count() / s.CourseItems.Count
                })
                .ToArray(),
            QuizResults = quizResults,
            TotalProgress = Math.Round((double)learning.Progresses
                .DistinctBy(p => p.CourseItemId)
                .Count() / course.Items.Count * 100),
            TotalScore = quizResults.Count == 0 ? 0 :(double)quizResults.Select(r => r.ResultPercent).Sum() / quizResults.Count
        };
    }
}