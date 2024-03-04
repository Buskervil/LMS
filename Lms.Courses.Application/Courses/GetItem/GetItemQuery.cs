using Courses.Application.Core;

namespace Courses.Application.Courses.GetItem;

public sealed class GetItemQuery : IQuery<object>
{
    public Guid CourseId { get; }
    public Guid ItemId { get; }
    
    public GetItemQuery(Guid courseId, Guid itemId)
    {
        CourseId = courseId;
        ItemId = itemId;
    }
}