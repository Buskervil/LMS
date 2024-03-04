using Courses.Application.Core;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;

namespace Courses.Application.Courses.AddVideo;

// ReSharper disable once UnusedType.Global
internal sealed class AddVideoCommandHandler : ICommandHandler<AddVideoCommand, Guid>
{
    private readonly ICourseRepository _courseRepository;

    public AddVideoCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<Guid>> Handle(AddVideoCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(CourseId.Create(request.CourseId));
        if (course == null)
        {
            return Result.Failure<Guid>(ApiError.BadRequest($"Курс с id {course} не найден"));
        }

        var result = course.AddVideo(EntityName.Create(request.Name),
            request.SectionId,
            request.SourceLink,
            request.PreviousItemId);

        if (result.IsSuccess)
        {
            _courseRepository.Update(course);
        }

        return result;
    }
}