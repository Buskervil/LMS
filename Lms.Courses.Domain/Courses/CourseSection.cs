using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses.ValueObjects;

namespace Lms.Courses.Domain.Courses;

public class CourseSection : Entity
{
    private List<CourseItem> _courseItems = new();

    public Guid Id { get; private set; }
    public CourseId CourseId { get; private set; }
    public EntityName Name { get; private set; }
    public EntityDescription Description { get; private set; }
    public Guid AuthorId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public Guid? PreviousSection { get; private set; }

    public IReadOnlyCollection<CourseItem> CourseItems => _courseItems.ToList();

    private CourseSection()
    {
    }

    private CourseSection(Guid id,
        CourseId courseId,
        EntityName name,
        EntityDescription description,
        Guid authorId,
        DateTimeOffset createdAt,
        Guid? previousSection)
    {
        Id = id;
        CourseId = courseId;
        Name = name;
        Description = description;
        AuthorId = authorId;
        CreatedAt = createdAt;
        PreviousSection = previousSection;
    }

    public static CourseSection Create(CourseId courseId,
        EntityName name,
        EntityDescription description,
        Guid authorId,
        Guid? previousSection = null)
    {
        return new CourseSection(Guid.NewGuid(), courseId, name, description, authorId, DateTimeOffset.UtcNow, previousSection);
    }

    public Result Edit(EntityName name, EntityDescription description)
    {
        Name = name;
        Description = description;

        return Result.Success();
    }

    #region Overrides

    public override bool Equals(Entity other)
    {
        var section = other as CourseSection;

        return section != null && section.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion

    internal Result<Guid> AddArticle(EntityName name, ArticleContent content, Guid? previousItemId = null)
    {
        if (previousItemId.HasValue)
        {
            var previousItem = _courseItems.FirstOrDefault(t => t.Id == previousItemId);
            if (previousItem == null)
            {
                return Result.Failure<Guid>(ApiError.BadRequest($"Не найден предыдущий элемент с id {previousItemId} в разделе"));
            }
        }

        return Article.Create(name, Id, content, previousItemId).Map(a => a.Id);
    }
    
    internal Result EditArticle(Guid articleId, EntityName name, ArticleContent content)
    {
        if (_courseItems.FirstOrDefault(t => t.Id == articleId) is not Article article)
        {
            return Result.Failure(ApiError.BadRequest($"Не найдена статья с id {articleId}"));
        }

        return article.Edit(name, content);
    }
}