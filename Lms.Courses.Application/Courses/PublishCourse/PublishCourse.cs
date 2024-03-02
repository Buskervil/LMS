using Courses.Application.Core;

namespace Courses.Application.Courses.PublishCourse;

public sealed class PublishCourseCommand : ICommand
{
    public Guid CourseId { get; }
    
    public PublishCourseCommand(Guid courseId)
    {
        CourseId = courseId;
    }
}