using Courses.Application.Core;
using Lms.Core.Application.Sessions;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;
using Lms.Courses.Domain.Learnings;

namespace Courses.Application.Learnings.CommitQuiz;

// ReSharper disable once UnusedType.Global
internal sealed class CommitQuizCommandHandler : ICommandHandler<CommitQuizCommand, int>
{
    private readonly ISessionProvider _sessionProvider;
    private readonly ICourseRepository _courseRepository;
    private readonly ILearningRepository _learningRepository;

    public CommitQuizCommandHandler(ISessionProvider sessionProvider,
        ICourseRepository courseRepository,
        ILearningRepository learningRepository)
    {
        _sessionProvider = sessionProvider;
        _courseRepository = courseRepository;
        _learningRepository = learningRepository;
    }

    public async Task<Result<int>> Handle(CommitQuizCommand request, CancellationToken cancellationToken)
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

        var commitItemResult = learning.CommitQuiz(course, request.QuizId, request.SolvedQuestions);
        if (commitItemResult.IsSuccess)
        {
            _learningRepository.Update(learning);
        }

        return commitItemResult.Value.Value;
    }
}