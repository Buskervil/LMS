using Courses.Application.Core;
using Lms.Core.Application.Sessions;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;
using Lms.Courses.Domain.Learnings;

namespace Courses.Application.Learnings.StartLearnCourse;

// ReSharper disable once UnusedType.Global
internal sealed class StartLearnCourseCommandHandler : ICommandHandler<StartLearnCourseCommand>
{
    private readonly ISessionProvider _sessionProvider;
    private readonly ICourseRepository _courseRepository;
    private readonly ILearningRepository _learningRepository;

    public StartLearnCourseCommandHandler(ISessionProvider sessionProvider,
        ICourseRepository courseRepository,
        ILearningRepository learningRepository)
    {
        _sessionProvider = sessionProvider;
        _courseRepository = courseRepository;
        _learningRepository = learningRepository;
    }

    public async Task<Result> Handle(StartLearnCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(CourseId.Create(request.CourseId));
        if (course == null)
        {
            return Result.Failure(ApiError.BadRequest($"Курс с id {request.CourseId} не существует"));
        }

        var learningResult = Learning.StartLearnCourse(_sessionProvider.UserId, course);
        if (learningResult.IsSuccess)
        {
            _learningRepository.Add(learningResult.Value);
        }

        return Result.Success();
    }
}