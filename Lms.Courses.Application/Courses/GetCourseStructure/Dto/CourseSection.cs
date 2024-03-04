namespace Courses.Application.Courses.GetCourseStructure.Dto;

public class CourseSection
{
    public Guid Id { get; init; }
    public Guid? PreviousSectionId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public IEnumerable<CourseItem> Items { get; init;}
}