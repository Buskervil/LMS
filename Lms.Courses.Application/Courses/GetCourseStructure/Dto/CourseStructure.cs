using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Learnings;

namespace Courses.Application.Courses.GetCourseStructure.Dto;

public sealed class CourseStructure
{
    public Guid CourseId { get; init; }
    public string CourseName { get; init; }
    public string CourseDescription { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public int Duration { get; init; }
    public CourseSection[] Sections { get; init; }
    public DateTimeOffset? Deadline { get; init; }
    public Guid? LearningId { get; init; }
    public Guid? LastCommittedItem { get; init; }

    public static CourseStructure Create(Course course, Learning? learning)
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
                                    Quiz => ItemType.Quiz,
                                    _ => throw new ArgumentOutOfRangeException(nameof(item), item, null)
                                },
                                IsCommitted = learning?.IsItemCommitted(item.Id) ?? false
                            };
                }).ToList()
            }).ToArray(),
            Deadline = learning?.Deadline,
            LearningId = learning?.Id,
            LastCommittedItem = learning?.GetLastCommittedItem()
        };
    }
}