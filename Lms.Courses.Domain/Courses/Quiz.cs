using Lms.Courses.Domain.Courses.ValueObjects;
using Lms.Courses.Domain.Dto;
using Lms.Courses.Domain.Learnings.ValueObjects;

namespace Lms.Courses.Domain.Courses;

public class Quiz : CourseItem
{
    private List<Question> _questions = new();

    public IReadOnlyCollection<Question> Questions => _questions.AsReadOnly();

    public Quiz(Guid id, EntityName name, Guid courseSectionId, DateTimeOffset createdAt, Guid? previousItemId) : base(id,
        name,
        courseSectionId,
        createdAt,
        previousItemId)
    {
    }

    public Percent GetScore(IEnumerable<SolvedQuestion> solvedQuestions)
    {
        var scoresSum = 0;
        foreach (var solvedQuestion in solvedQuestions)
        {
            var question = _questions.First(q => q.Id == solvedQuestion.Id);
            var questionScore = question.GetScore(solvedQuestion.SolvedAnswers);

            scoresSum += questionScore;
        }

        var result = (double)scoresSum / (_questions.Count * 100);
        
        return Percent.FromDouble(result);
    }
}