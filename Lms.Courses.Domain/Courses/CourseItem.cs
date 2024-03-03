using Lms.Core.Domain.Primitives;
using Lms.Courses.Domain.Courses.ValueObjects;
using Lms.Courses.Domain.Learnings.ValueObjects;

namespace Lms.Courses.Domain.Courses;

public abstract class CourseItem : Entity
{
    public Guid Id { get; private set; }
    public EntityName Name { get; protected set; }
    public Guid CourseSectionId { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public Guid? PreviousItemId { get; protected set; }

    protected CourseItem(Guid id, EntityName name, Guid courseSectionId, DateTimeOffset createdAt, Guid? previousItemId)
    {
        Id = id;
        Name = name;
        CourseSectionId = courseSectionId;
        CreatedAt = createdAt;
        PreviousItemId = previousItemId;
    }

    public abstract Percent GetScore();
    
    #region Overrides

    public override bool Equals(Entity other)
    {
        var courseItem = other as CourseItem;

        return courseItem != null && courseItem.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion
}