using Lms.Core.Domain.Primitives;

namespace Lms.Courses.Domain.Courses;

public sealed class Answer : Entity
{
    public Guid Id { get; private set; }
    public Guid QuestionId { get; private set; }
    public string Content { get; private set; }
    public bool IsCorrect { get; private set; }

    #region Overrides

    public override bool Equals(Entity other)
    {
        var otherAnswer = other as Answer;

        return otherAnswer != null && otherAnswer.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion
}