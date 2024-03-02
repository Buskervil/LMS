using Courses.Application.Core;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Course;
using Lms.Courses.Domain.Course.ValueObjects;

namespace Courses.Application.Courses.PublishCourse;

// ReSharper disable once UnusedType.Global
internal sealed class PublishCourseCommandHandler : ICommandHandler<PublishCourseCommand>
{
    private readonly ICourseRepository _courseRepository;

    public PublishCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result> Handle(PublishCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(CourseId.Create(request.CourseId));
        if (course == null)
        {
            return Result.Failure(ApiError.BadRequest($"Курс с id {request.CourseId} не существует"));
        }
        
        course.Publish();
        
        _courseRepository.Update(course);
        
        return Result.Success();
    }
}