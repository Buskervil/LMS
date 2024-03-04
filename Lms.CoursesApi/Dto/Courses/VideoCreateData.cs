namespace Lms.CoursesApi.Dto.Courses;

public class VideoCreateData
{
    public string Name { get; set; }
    public Guid SectionId { get; set; }
    public Guid CourseId { get; set; }
    public Guid? PreviousSectionId { get; set; }
    public string SourceLink { get; set; }
}