﻿using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses.ValueObjects;

namespace Lms.Courses.Domain.Courses;

public class Course : AggregateRoot
{
    private List<CourseSection> _courseSections = new();

    public CourseId Id { get; private set; }
    public EntityName Name { get; private set; }
    public EntityDescription Description { get; private set; }
    public Guid OwnerId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public bool Published { get; private set; }
    public TimeSpan Duration { get; private set; }

    public IReadOnlyCollection<CourseSection> CourseSections => _courseSections.ToList();
    public IReadOnlyCollection<CourseItem> Items => _courseSections.SelectMany(s => s.CourseItems).ToArray();

    private Course()
    {
    }

    private Course(CourseId id, EntityName name, EntityDescription description, Guid ownerId, DateTimeOffset createdAt, bool published)
    {
        Id = id;
        Name = name;
        Description = description;
        OwnerId = ownerId;
        CreatedAt = createdAt;
    }

    public static Course Create(EntityName name,
        EntityDescription description,
        Guid ownerId)
    {
        return new Course(CourseId.Create(), name, description, ownerId, DateTimeOffset.UtcNow, false);
    }

    public Result<Guid> AddSection(EntityName name,
        EntityDescription description,
        Guid authorId,
        Guid? previousSectionId)
    {
        if (previousSectionId.HasValue)
        {
            var previousSection = _courseSections.FirstOrDefault(s => s.Id == previousSectionId);
            if (previousSection == null)
            {
                return Result.Failure<Guid>(ApiError.BadRequest($"Не найден предыдущий раздел курса с id {previousSection}"));
            }
        }

        var section = CourseSection.Create(Id, name, description, authorId);
        _courseSections.Add(section);

        return section.Id;
    }

    public Result EditSection(Guid sectionId, EntityName name, EntityDescription description)
    {
        var section = _courseSections.FirstOrDefault(s => s.Id == sectionId);
        if (section == null)
        {
            return Result.Failure(ApiError.BadRequest($"Не найден раздел курса с id {sectionId}"));
        }

        return section.Edit(name, description);
    }

    public Result<Guid> AddArticle(EntityName name, Guid sectionId, ArticleContent content, Guid? previousItemId)
    {
        var section = _courseSections.FirstOrDefault(s => s.Id == sectionId);
        if (section == null)
        {
            return Result.Failure<Guid>(ApiError.BadRequest($"Не найден раздел курса с id {section}"));
        }

        return section.AddArticle(name, content, previousItemId);
    }
    
    public Result<Guid> AddVideo(EntityName name, Guid sectionId, string source, Guid? previousItemId)
    {
        var section = _courseSections.FirstOrDefault(s => s.Id == sectionId);
        if (section == null)
        {
            return Result.Failure<Guid>(ApiError.BadRequest($"Не найден раздел курса с id {section}"));
        }

        return section.AddVideo(name, source, previousItemId);
    }
    
    public Result EditArticle(Guid articleId, Guid sectionId, EntityName name, ArticleContent content)
    {
        var section = _courseSections.FirstOrDefault(s => s.Id == sectionId);
        if (section == null)
        {
            return Result.Failure(ApiError.BadRequest($"Не найден раздел курса с id {section}"));
        }

        return section.EditArticle(articleId, name, content);
    }

    public void Publish()
    {
        Published = true;
    }

    public void Hide()
    {
        Published = false;
    }

    public CourseSection[] GetSectionsInOrder()
    {
        if (_courseSections.Count == 0)
        {
            return Array.Empty<CourseSection>();
        }
        
        var sectionsByPreviousId = CourseSections
            .ToDictionary(s => s.PreviousSection ?? Guid.Empty, s => s);

        var sections = new CourseSection[CourseSections.Count];
        sections[0] = sectionsByPreviousId[Guid.Empty];
        for (var i = 1; i < sections.Length; i++)
        {
            sections[i] = sectionsByPreviousId[sections[i - 1].Id];
        }

        return sections;
    }
    
    #region Overrides

    public override bool Equals(Entity other)
    {
        var course = other as Course;

        return course != null && course.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion
}