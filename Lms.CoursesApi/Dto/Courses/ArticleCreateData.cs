namespace Lms.CoursesApi.Dto.Courses;

public class ArticleCreateData
{
    public string Name { get; set; }
    public Guid SectionId { get; set; }
    public Guid CourseId { get; set; }
    public Guid? PreviousSectionId { get; set; }
    public string Content { get; set; }
}