using Courses.Domain.Course.ValueObjects;

namespace Courses.Domain.Course;

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
}