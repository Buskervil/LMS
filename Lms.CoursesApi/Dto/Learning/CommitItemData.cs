namespace Lms.CoursesApi.Dto.Learning;

public class CommitItemData
{
    public Guid LearningId { get; set; }
    public Guid CourseId { get; set; }
    public Guid ItemId { get; set; }
}

public sealed class CommitQuizData
{
    public Guid LearningId { get; set; }
    public Guid QuizId { get;  set;}
    public Guid CourseId { get; set; }
    public List<SolvedQuestion> SolvedQuestions { get; set; }
}

public class SolvedQuestion
{
    public Guid Id { get; set; }
    public List<SolvedAnswer> SolvedAnswers { get; set; }
}

public class SolvedAnswer
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public bool IsSelected { get; set; }
}