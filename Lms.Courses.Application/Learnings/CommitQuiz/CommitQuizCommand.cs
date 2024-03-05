using Courses.Application.Core;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Dto;

namespace Courses.Application.Learnings.CommitQuiz;

public sealed class CommitQuizCommand : ICommand<int>
{
    public Guid LearningId { get; }
    public Guid QuizId { get; }
    public Guid CourseId { get; }
    public IReadOnlyCollection<SolvedQuestion> SolvedQuestions { get; }

    public CommitQuizCommand(Guid courseId, Guid quizId, Guid learningId, IReadOnlyCollection<SolvedQuestion> solvedQuestions)
    {
        CourseId = courseId;
        QuizId = quizId;
        SolvedQuestions = solvedQuestions;
        LearningId = learningId;
    }
}