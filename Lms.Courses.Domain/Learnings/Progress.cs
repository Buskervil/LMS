using Lms.Core.Domain.Primitives;
using Lms.Courses.Domain.Learnings.ValueObjects;

namespace Lms.Courses.Domain.Learnings;

public sealed class Progress : Entity
{
    public Guid Id { get; private set; }
    public Guid LearningId { get; private set; }
    public Guid CourseItemId { get; private set; }
    public Percent ScoreInPercent { get; private set; }
    public DateTimeOffset CommittedAt { get; private set; }

    private Progress(Guid id, Guid learningId, Guid courseItemId, Percent scoreInPercent, DateTimeOffset committedAt)
    {
        Id = id;
        LearningId = learningId;
        CourseItemId = courseItemId;
        ScoreInPercent = scoreInPercent;
        CommittedAt = committedAt;
    }

    private Progress()
    {
    }

    public static Progress Create(Guid learningId, Guid courseItemId, Percent scoreInPercent)
    {
        return new Progress(Guid.NewGuid(), learningId, courseItemId, scoreInPercent, DateTimeOffset.UtcNow);
    }

    #region Overrides

    public override bool Equals(Entity other)
    {
        var otherProgress = other as Progress;

        return otherProgress != null && otherProgress.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion
}