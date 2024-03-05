using Courses.Application.Core;
using Courses.Application.Courses.GetCourseStructure.Dto;
using Lms.Core.Application.Sessions;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses.ValueObjects;

namespace Courses.Application.Courses.GetCourseStructure;

// ReSharper disable once UnusedType.Global
internal sealed class GetCourseStructureQueryHandler : IQueryHandler<GetCourseStructureQuery, CourseStructure>
{
    private readonly ICoursesReadRepository _coursesReadRepository;
    private readonly ISessionProvider _sessionProvider;

    public GetCourseStructureQueryHandler(ICoursesReadRepository coursesReadRepository, ISessionProvider sessionProvider)
    {
        _coursesReadRepository = coursesReadRepository;
        _sessionProvider = sessionProvider;
    }

    public async Task<Result<CourseStructure>> Handle(GetCourseStructureQuery request, CancellationToken cancellationToken)
    {
        var course = await _coursesReadRepository.GetCourse(CourseId.Create(request.CourseId));
        if (course == null)
        {
            return Result.Failure<CourseStructure>(ApiError.BadRequest($"Курс с id {request.CourseId} не существует"));
        }

        var learnings = await _coursesReadRepository.GetLearningByCourse(course.Id, _sessionProvider.UserId);

        var courseData = CourseStructure.Create(course, learnings);
        return courseData;
    }
}