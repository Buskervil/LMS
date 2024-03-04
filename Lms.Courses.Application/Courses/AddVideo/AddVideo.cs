using Courses.Application.Core;

namespace Courses.Application.Courses.AddVideo;

public sealed class AddVideoCommand : ICommand<Guid>
{
    public string Name { get; }
    public Guid SectionId { get; set; }
    public Guid CourseId { get; }
    public Guid? PreviousItemId { get; }
    public string SourceLink { get; }

    public AddVideoCommand(string name, Guid sectionId, Guid courseId, Guid? previousItemId, string sourceLink)
    {
        Name = name;
        SectionId = sectionId;
        CourseId = courseId;
        PreviousItemId = previousItemId;
        SourceLink = sourceLink;
    }
}