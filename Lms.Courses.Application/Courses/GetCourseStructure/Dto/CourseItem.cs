namespace Courses.Application.Courses.GetCourseStructure.Dto;

public class CourseItem
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Guid? PreviousItemId { get; init; }
    public ItemType Type { get; init; }
}