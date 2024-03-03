using Courses.Application.Core;
using Lms.Core.Application.Sessions;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;
using Lms.Courses.Domain.Learnings;

namespace Courses.Application.Learnings.CommitItem;

// ReSharper disable once UnusedType.Global
internal sealed class CommitItemCommandHandler : ICommandHandler<CommitItemCommand, int>
{
    private readonly ILearningRepository _learningRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ISessionProvider _sessionProvider;

    public CommitItemCommandHandler(ILearningRepository learningRepository,
        ICourseRepository courseRepository,
        ISessionProvider sessionProvider)
    {
        _learningRepository = learningRepository;
        _courseRepository = courseRepository;
        _sessionProvider = sessionProvider;
    }

    public async Task<Result<int>> Handle(CommitItemCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(CourseId.Create(request.CourseId));
        if (course == null)
        {
            return Result.Failure<int>(ApiError.BadRequest($"Курс с id {request.CourseId} не существует"));
        }

        var learning = await _learningRepository.GetByIdAsync(request.LearningId);
        if (learning == null || learning.StudentId != _sessionProvider.UserId)
        {
            return Result.Failure<int>(ApiError.BadRequest($"Вы не изучаете курс {course.Name}"));
        }

        var commitItemResult = learning.CommitItem(course, request.ItemId);
        if (commitItemResult.IsSuccess)
        {
            _learningRepository.Update(learning);
        }

        return commitItemResult.Value.Value;
    }
}