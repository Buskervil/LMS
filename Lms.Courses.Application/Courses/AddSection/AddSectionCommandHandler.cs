using Courses.Application.Core;
using Lms.Core.Application.Sessions;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Course;
using Lms.Courses.Domain.Course.ValueObjects;

namespace Courses.Application.Courses.AddSection;

// ReSharper disable once UnusedType.Global
internal sealed class AddSectionCommandHandler : ICommandHandler<AddSectionCommand, Guid>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ISessionProvider _sessionProvider;

    public AddSectionCommandHandler(ICourseRepository courseRepository, ISessionProvider sessionProvider)
    {
        _courseRepository = courseRepository;
        _sessionProvider = sessionProvider;
    }

    public async Task<Result<Guid>> Handle(AddSectionCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(CourseId.Create(request.CourseId));
        if (course == null)
        {
            return Result.Failure<Guid>(ApiError.BadRequest($"Курс с id {course} не найден"));
        }

        var result = course.AddSection(EntityName.Create(request.Name),
            EntityDescription.Create(request.Description),
            _sessionProvider.UserId,
            request.PreviousSectionId);

        if (result.IsSuccess)
        {
            _courseRepository.Update(course);
        }

        return result;
    }
}