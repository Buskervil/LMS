namespace Lms.CoursesApi.Dto;

public class CourseCreateData
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class SectionCreateData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid CourseId { get; set; }
    public Guid? PreviousSectionId { get; set; }
}

public class ArticleCreateData
{
    public string Name { get; set; }
    public Guid SectionId { get; set; }
    public Guid CourseId { get; set; }
    public Guid? PreviousSectionId { get; set; }
    public string Content { get; set; }
}