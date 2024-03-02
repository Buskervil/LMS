using Courses.Application.Core;

namespace Courses.Application.Courses.AddSection;

public sealed class AddSectionCommand : ICommand<Guid>
{
    public string Name { get; }
    public string Description { get; }
    public Guid CourseId { get; }
    public Guid? PreviousSectionId { get; }

    public AddSectionCommand(string name, string description, Guid courseId, Guid? previousSectionId)
    {
        Name = name;
        Description = description;
        CourseId = courseId;
        PreviousSectionId = previousSectionId;
    }
}