using Courses.Application.Core;

namespace Courses.Application.Learnings.StartLearnCourse;

public sealed class StartLearnCourseCommand : ICommand
{
    public Guid CourseId { get; }

    public StartLearnCourseCommand(Guid courseId)
    {
        CourseId = courseId;
    }
}