using Lms.Core.Domain.Primitives;
using Lms.Courses.Domain.Dto;
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

        foreach (var answer in _answers)
        {
            var solved = solvedAnswers.FirstOrDefault(a => a.Id == answer.Id);
            if (answer.IsCorrect == false && (solved == null || solved.IsSelected == false))
            {
                correctAnswers++;
            }
            else if (answer.IsCorrect && solved != null && solved.IsSelected)
            {
                correctAnswers++;
            }
        }

        var result = (double)correctAnswers / _answers.Count;
        
        return Percent.FromDouble(result);
    }
}