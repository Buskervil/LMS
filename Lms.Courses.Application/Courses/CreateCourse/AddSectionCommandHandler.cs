using Courses.Application.Core;
using Lms.Core.Application.Sessions;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Course;
using Lms.Courses.Domain.Course.ValueObjects;

namespace Courses.Application.Courses.CreateCourse;

// ReSharper disable once UnusedType.Global
internal sealed class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, Guid>
{
    private readonly ICourseRepository _courseRepository;
    private readonly ISessionProvider _sessionProvider;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, ISessionProvider sessionProvider)
    {
        _courseRepository = courseRepository;
        _sessionProvider = sessionProvider;
    }

    public Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var result = Course.Create(EntityName.Create(request.Name), EntityDescription.Create(request.Description), _sessionProvider.UserId);

        _courseRepository.Add(result);

        return Task.FromResult<Result<Guid>>(result.Id.Value);
    }
}