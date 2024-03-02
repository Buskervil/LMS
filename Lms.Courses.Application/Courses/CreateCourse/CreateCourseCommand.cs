using Courses.Application.Core;

namespace Courses.Application.Courses.CreateCourse;

public sealed class CreateCourseCommand : ICommand<Guid>
{
    public string Name { get; }
    public string Description { get; }

    public CreateCourseCommand(string name, string description)
    {
        Name = name;
        Description = description;
    }
}