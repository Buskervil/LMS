using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Course.ValueObjects;

namespace Lms.Courses.Domain.Course;

public class Article : CourseItem
{
    public ArticleContent Content { get; private set; }

    private Article(Guid id,
        EntityName name,
        Guid courseSectionId,
        DateTimeOffset createdAt,
        ArticleContent content,
        Guid? previousItemId) : base(id,
        name,
        courseSectionId,
        createdAt,
        previousItemId)
    {
        Content = content;
    }

    public static Result<Article> Create(EntityName name, Guid courseSectionId, ArticleContent content, Guid? previousItemId)
    {
        return new Article(Guid.NewGuid(), name, courseSectionId, DateTimeOffset.UtcNow, content, previousItemId);
    }

    public Result Edit(EntityName name, ArticleContent content)
    {
        Name = name;
        Content = content;

        return Result.Success();
    }
}