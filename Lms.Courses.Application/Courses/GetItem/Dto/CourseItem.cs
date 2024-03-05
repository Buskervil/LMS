using Courses.Application.Courses.GetCourseStructure.Dto;

namespace Courses.Application.Courses.GetItem.Dto;

public class CourseItem
{
    public string Name { get; init; }
    public string Content { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public ItemType Type { get; init; }
    public string Source { get; init; }
    public IEnumerable<Question> Questions { get; init; }
}