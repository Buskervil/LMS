using Courses.Application.Core;

namespace Courses.Application.Learnings.AssignCourseToUser;

public sealed class AssignToUserCommand : ICommand
{
    public Guid CourseId { get; }
    public Guid StudentId { get; }
    
    public AssignToUserCommand(Guid courseId, Guid studentId)
    {
        CourseId = courseId;
        StudentId = studentId;
    }
}