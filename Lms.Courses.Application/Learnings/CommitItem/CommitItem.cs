using Courses.Application.Core;

namespace Courses.Application.Learnings.CommitItem;

public sealed class CommitItemCommand : ICommand<int>
{
    public Guid LearningId { get; }
    public Guid CourseId { get; }
    public Guid ItemId { get; }

    public CommitItemCommand(Guid courseId, Guid itemId, Guid learningId)
    {
        CourseId = courseId;
        ItemId = itemId;
        LearningId = learningId;
    }
}