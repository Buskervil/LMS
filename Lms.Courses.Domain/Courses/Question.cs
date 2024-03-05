using Lms.Core.Domain.Primitives;
using Lms.Courses.Domain.Learnings.ValueObjects;

namespace Lms.Courses.Domain.Courses;

public sealed class Question : Entity
{
    private List<Answer> _answers = new();

    public Guid Id { get; private set; }
    public Guid QuizId { get; private set; }
    public string Content { get; private set; }
    public bool AllowMultipleAnswers { get; private set; }

    public IReadOnlyCollection<Answer> Answers => _answers.AsReadOnly();

    #region Overrides

    public override bool Equals(Entity other)
    {
        var otherQuestion = other as Question;

        return otherQuestion != null && otherQuestion.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    #endregion

    public Percent GetScore(IEnumerable<SolvedAnswer> solvedAnswers)
    {
        var correctAnswers = 0;
        foreach (var solvedAnswer in solvedAnswers)
        {
            var answer = _answers.First(a => a.Id == solvedAnswer.Id);
            if (answer.IsCorrect && solvedAnswer.IsSelected)
            {
                correctAnswers++;
            }
        }

        var result = (double)correctAnswers / _answers.Count;
        
        return Percent.FromDouble(result);
    }
}