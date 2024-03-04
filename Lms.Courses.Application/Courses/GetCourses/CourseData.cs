namespace Courses.Application.Courses.GetCourses;

public sealed class CourseData
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Duration { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public bool Started { get; private set; }
    public bool Ended { get; private set; }
}