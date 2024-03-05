using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Learnings.ValueObjects;

namespace Lms.Courses.Domain.Learnings;

public sealed class Learning : Entity
{
    private readonly List<Progress> _progresses = new();

    public Guid Id { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid StudentId { get; private set; }
    public DateTimeOffset StartedAt { get; private set; }
    public DateTimeOffset? FinishedAt { get; private set; }
    public DateTimeOffset Deadline { get; private set; }
    public Guid? AssignedBy { get; private set; }

    public IReadOnlyCollection<Progress> Progresses => _progresses.ToArray();

    public Learning(Guid id,
        Guid courseId,
        Guid studentId,
        DateTimeOffset startedAt,
        DateTimeOffset deadline,
        Guid? assignedBy = null)
    {
        Id = id;
        CourseId = courseId;
        StudentId = studentId;
        StartedAt = startedAt;
        Deadline = deadline;
        AssignedBy = assignedBy;
    }

    private Learning()
    {
    }

    public static Result<Learning> AssignToUser(Guid studentId, Guid assignerId, Course course)
    {
        if (course.Published == false)
        {
            return Result.Failure<Learning>(ApiError.BadRequest("Нельзя назначить сотруднику неопубликованный курс"));
        }

        var assignedAt = DateTimeOffset.UtcNow;
        var deadline = assignedAt.Add(course.Duration);
        var learning = new Learning(Guid.NewGuid(), course.Id, studentId, assignedAt, deadline, assignerId);
        return learning;
    }

    public static Result<Learning> StartLearnCourse(Guid studentId, Course course)
    {
        if (course.Published == false)
        {
            return Result.Failure<Learning>(ApiError.BadRequest("Курс не существует, либо не опубликован"));
        }

        var assignedAt = DateTimeOffset.UtcNow;
        var deadline = assignedAt.Add(course.Duration);
        var learning = new Learning(Guid.NewGuid(), course.Id, studentId, assignedAt, deadline);
        return learning;
    }

    public Result<Percent> CommitItem(Course course, Guid itemId)
    {
        var item = course.Items.FirstOrDefault(t => t.Id == itemId);
        if (item == null)
        {
            return Result.Failure<Percent>(ApiError.BadRequest($"Элемент курса с id {itemId} не найден"));
        }

        var scoreInPercent = Percent.Create(100);
        var progress = Progress.Create(Id, itemId, scoreInPercent);
        
        _progresses.Add(progress);
        
        return scoreInPercent;
    }
    
    public Result<Percent> CommitQuiz(Course course, Guid itemId, IEnumerable<SolvedQuestion> solvedQuestions)
    {
        if (course.Items.FirstOrDefault(t => t.Id == itemId) is not Quiz quiz)
        {
            return Result.Failure<Percent>(ApiError.BadRequest($"Элемент курса с id {itemId} не найден"));
        }

        var score = quiz.GetScore(solvedQuestions);
        var progress = Progress.Create(Id, itemId, score);
        
        _progresses.Add(progress);
        
        return score;
    }

    public Guid? GetLastCommittedItem()
    {
        return _progresses.MaxBy(p => p.CommittedAt)?.Id;
    }
    
    public bool IsItemCommitted(Guid itemId)
    {
        return _progresses.Any(p => p.CourseItemId == itemId);
    }

    #region Overrides

    public override bool Equals(Entity other)
    {
        var otherLearning = other as Learning;

        return otherLearning != null && otherLearning.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion
}