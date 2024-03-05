using Courses.Application.Core;
using Courses.Application.Courses.GetItem.Dto;

namespace Courses.Application.Courses.GetItem;

public sealed class GetItemQuery : IQuery<CourseItem>
{
    public Guid CourseId { get; }
    public Guid ItemId { get; }
    
    public GetItemQuery(Guid courseId, Guid itemId)
    {
        CourseId = courseId;
        ItemId = itemId;
    }
}