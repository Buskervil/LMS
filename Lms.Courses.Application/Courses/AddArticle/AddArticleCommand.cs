using Courses.Application.Core;

namespace Courses.Application.Courses.AddArticle;

public sealed class AddArticleCommand : ICommand<Guid>
{
    public string Name { get; }
    public Guid SectionId { get; set; }
    public Guid CourseId { get; }
    public Guid? PreviousItemId { get; }
    public string Content { get; }

    public AddArticleCommand(string name, Guid sectionId, Guid courseId, Guid? previousItemId, string content)
    {
        Name = name;
        SectionId = sectionId;
        CourseId = courseId;
        PreviousItemId = previousItemId;
        Content = content;
    }
}