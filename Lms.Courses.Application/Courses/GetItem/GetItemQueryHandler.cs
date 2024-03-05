using Courses.Application.Core;
using Courses.Application.Courses.GetCourseStructure.Dto;
using Lms.Core.Domain.Primitives;
using Lms.Core.Domain.Results;
using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;
using Answer = Courses.Application.Courses.GetItem.Dto.Answer;
using CourseItem = Courses.Application.Courses.GetItem.Dto.CourseItem;
using Question = Courses.Application.Courses.GetItem.Dto.Question;

namespace Courses.Application.Courses.GetItem;

// ReSharper disable once UnusedType.Global
internal sealed class GetItemQueryHandler : IQueryHandler<GetItemQuery, CourseItem>
{
    private readonly ICoursesReadRepository _coursesReadRepository;

    public GetItemQueryHandler(ICoursesReadRepository coursesReadRepository)
    {
        _coursesReadRepository = coursesReadRepository;
    }

    public async Task<Result<CourseItem>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var course = await _coursesReadRepository.GetCourse(CourseId.Create(request.CourseId));
        if (course == null)
        {
            return Result.Failure<CourseItem>(ApiError.BadRequest($"Курс с id {request.CourseId} не существует"));
        }

        var item = course.Items.FirstOrDefault(i => i.Id == request.ItemId);
        if (item == null)
        {
            return Result.Failure<CourseItem>(ApiError.BadRequest($"Элемент курса с id {request.ItemId} не существует"));
        }

        switch (item)
        {
            case Article article:
                return new CourseItem
                {
                    Name = article.Name.Value,
                    Content = article.Content.Value,
                    CreatedAt = article.CreatedAt,
                    Type = ItemType.Article
                };

            case Video video:
                return new CourseItem
                {
                    Name = video.Name.Value,
                    Source = video.ContentLink,
                    CreatedAt = video.CreatedAt,
                    Type = ItemType.Video
                };

            case Quiz quiz:
                var withDataQuiz = await _coursesReadRepository.GetQuiz(quiz.Id);
                return new CourseItem
                {
                    Name = quiz.Name.Value,
                    Type = ItemType.Quiz,
                    Questions = withDataQuiz!.Questions
                        .Select(q => new Question()
                        {
                            Id = q.Id,
                            QuizId = q.QuizId,
                            Content = q.Content,
                            AllowMultipleAnswers = q.AllowMultipleAnswers,
                            Answers = q.Answers
                                .Select(a => new Answer()
                                {
                                    Id = a.Id,
                                    Content = a.Content,
                                    QuestionId = a.QuestionId
                                })
                        })
                };
            default:
                throw new ArgumentOutOfRangeException(nameof(item));
        }
    }
}