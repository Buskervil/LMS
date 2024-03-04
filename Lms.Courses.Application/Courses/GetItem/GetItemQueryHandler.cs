using Courses.Application.Core;
using Courses.Application.Courses.GetCourseStructure.Dto;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;

namespace Courses.Application.Courses.GetItem;

// ReSharper disable once UnusedType.Global
internal sealed class GetItemQueryHandler : IQueryHandler<GetItemQuery, object>
{
    private readonly ICoursesReadRepository _coursesReadRepository;

    public GetItemQueryHandler(ICoursesReadRepository coursesReadRepository)
    {
        _coursesReadRepository = coursesReadRepository;
    }

    public async Task<Result<object>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var course = await _coursesReadRepository.GetCourse(CourseId.Create(request.CourseId));
        if (course == null)
        {
            return Result.Failure<object>(ApiError.BadRequest($"Курс с id {request.CourseId} не существует"));
        }

        var item = course.Items.FirstOrDefault(i => i.Id == request.ItemId);
        if (item == null)
        {
            return Result.Failure<object>(ApiError.BadRequest($"Элемент курса с id {request.ItemId} не существует"));
        }

        switch (item)
        {
            case Article article:
                return new { Name = article.Name.Value, Content = article.Content.Value, article.CreatedAt, Type = ItemType.Article };
            
            case Test test:
            case Video video:
            default:
                throw new ArgumentOutOfRangeException(nameof(item));
        }
    }
}