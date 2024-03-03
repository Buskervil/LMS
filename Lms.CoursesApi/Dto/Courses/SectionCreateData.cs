namespace Lms.CoursesApi.Dto.Courses;

public class SectionCreateData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CourseId { get; set; }
    public Guid? PreviousSectionId { get; set; }
}