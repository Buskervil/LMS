using Courses.Domain.Course.ValueObjects;

namespace Courses.Domain.Course;

public class Test : CourseItem
{
    public Test(Guid id, EntityName name, Guid courseSectionId, DateTimeOffset createdAt, Guid? previousItemId) : base(id,
        name,
        courseSectionId,
        createdAt,
        previousItemId)
    {
    }
}