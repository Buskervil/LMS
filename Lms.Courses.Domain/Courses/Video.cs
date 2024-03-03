using Lms.Courses.Domain.Courses.ValueObjects;
using Lms.Courses.Domain.Learnings.ValueObjects;

namespace Lms.Courses.Domain.Courses;

public class Video : CourseItem
{
    public string ContentLink { get; private set; }

    public Video(Guid id, EntityName name, Guid courseSectionId, DateTimeOffset createdAt, Guid? previousItemId) : base(id,
        name,
        courseSectionId,
        createdAt,
        previousItemId)
    {
    }

    public override Percent GetScore()
    {
        throw new NotImplementedException();
    }
}