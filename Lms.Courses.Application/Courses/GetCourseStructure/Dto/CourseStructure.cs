using Lms.Courses.Domain.Courses;

namespace Courses.Application.Courses.GetCourseStructure.Dto;

public sealed class CourseStructure
{
    public Guid CourseId { get; init; }
    public string CourseName { get; init; }
    public string CourseDescription { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public int Duration { get; init; }
    public CourseSection[] Sections { get; init; }

    public static CourseStructure Create(Course course)
    {
        return new CourseStructure
        {
            CourseId = course.Id,
            CourseDescription = course.Description,
            CourseName = course.Name,
            CreatedAt = course.CreatedAt,
            Duration = (int)course.Duration.TotalDays,
            Sections = course.GetSectionsInOrder().Select(s => new CourseSection
            {
                Description = s.Description,
                Id = s.Id,
                PreviousSectionId = s.PreviousSection,
                Name = s.Name,
                Items = s.GetItemsInOrder().Select(item =>
                {
                    return new CourseItem()
                            {
                                Name = item.Name,
                                Id = item.Id,
                                PreviousItemId = item.PreviousItemId,
                                Type = item switch
                                {
                                    Article => ItemType.Article,
                                    Video => ItemType.Video,
                                    Test => ItemType.Quiz,
                                    _ => throw new ArgumentOutOfRangeException(nameof(item), item, null)
                                }
                            };
                }).ToList()
            }).ToArray()
        };
    }
}