using Lms.Courses.Domain.Courses.ValueObjects;
using Lms.Courses.Domain.Learnings.ValueObjects;

namespace Lms.Courses.Domain.Courses;

public class Video : CourseItem
{
    public string ContentLink { get; private set; }

    public Video(Guid id, EntityName name, Guid courseSectionId, DateTimeOffset createdAt, Guid? previousItemId, string contentLink)
        : base(
        id,
        name,
        courseSectionId,
        createdAt,
        previousItemId)
    {
        ContentLink = contentLink;
    }

    public Percent GetScore()
    {
        return Percent.Create(100);
    }

    public static Video Create(EntityName name, Guid sectionId, string source, Guid? previousItemId)
    {
        return new Video(Guid.NewGuid(), name, sectionId, DateTimeOffset.UtcNow, previousItemId, source);
    }
}