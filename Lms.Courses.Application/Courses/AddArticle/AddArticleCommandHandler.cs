using Courses.Application.Core;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Course;
using Lms.Courses.Domain.Course.ValueObjects;

namespace Courses.Application.Courses.AddArticle;

// ReSharper disable once UnusedType.Global
internal sealed class AddArticleCommandHandler : ICommandHandler<AddArticleCommand, Guid>
{
    private readonly ICourseRepository _courseRepository;

    public AddArticleCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Result<Guid>> Handle(AddArticleCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(CourseId.Create(request.CourseId));
        if (course == null)
        {
            return Result.Failure<Guid>(ApiError.BadRequest($"Курс с id {course} не найден"));
        }

        var result = course.AddArticle(EntityName.Create(request.Name),
            request.SectionId,
            ArticleContent.Create(request.Content),
            request.PreviousItemId);

        if (result.IsSuccess)
        {
            _courseRepository.Update(course);
        }

        return result;
    }
}