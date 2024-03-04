using Courses.Application.Core;
using Courses.Application.Courses.GetCourseStructure.Dto;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses.ValueObjects;

namespace Courses.Application.Courses.GetCourseStructure;

// ReSharper disable once UnusedType.Global
internal sealed class GetCourseStructureQueryHandler : IQueryHandler<GetCourseStructureQuery, CourseStructure>
{
    private readonly ICoursesReadRepository _coursesReadRepository;

    public GetCourseStructureQueryHandler(ICoursesReadRepository coursesReadRepository)
    {
        _coursesReadRepository = coursesReadRepository;
    }

    public async Task<Result<CourseStructure>> Handle(GetCourseStructureQuery request, CancellationToken cancellationToken)
    {
        var course = await _coursesReadRepository.GetCourse(CourseId.Create(request.CourseId));
        if (course == null)
        {
            return Result.Failure<CourseStructure>(ApiError.BadRequest($"Курс с id {request.CourseId} не существует"));
        }

        var courseData = CourseStructure.Create(course);
        return courseData;
    }
}