using Courses.Application.Core;
using Courses.Application.Learnings.AssignCourseToUser;
using Courses.Application.Learnings.CommitItem;
using Courses.Application.Learnings.CommitQuiz;
using Courses.Application.Learnings.StartLearnCourse;
using Lms.Core.Api;
using Lms.Courses.Domain.Dto;
using Lms.CoursesApi.Dto.Learning;
using Microsoft.AspNetCore.Mvc;
using SolvedAnswer = Lms.Courses.Domain.Dto.SolvedAnswer;
using SolvedQuestion = Lms.Courses.Domain.Dto.SolvedQuestion;

namespace Lms.CoursesApi;

[Route("api/learning")]
public class LearningController : BaseController
{
    private readonly ICoursesModule _coursesModule;

    public LearningController(ICoursesModule coursesModule)
    {
        _coursesModule = coursesModule;
    }

    [HttpPost("AssignToUser")]
    public async Task<IActionResult> AssignToUser(AssignLearningData assignLearningData)
    {
        var command = new AssignToUserCommand(assignLearningData.CourseId, assignLearningData.UserId);
        var result = await _coursesModule.ExecuteCommandAsync(command);

        return result.IsSuccess ? Ok(result) : CreateErrorResponse(result.Error);
    }
    
    [HttpPost("StartLearning")]
    public async Task<IActionResult> StartLearning([FromBody] StartLearningData startLearningData)
    {
        var command = new StartLearnCourseCommand(startLearningData.CourseId);
        var result = await _coursesModule.ExecuteCommandAsync(command);

        return result.IsSuccess ? Ok(result) : CreateErrorResponse(result.Error);
    }
    
    [HttpPost("CommitItem")]
    public async Task<IActionResult> CommitItem([FromBody] CommitItemData commitItemData)
    {
        var command = new CommitItemCommand(commitItemData.CourseId, commitItemData.ItemId, commitItemData.LearningId);
        var result = await _coursesModule.ExecuteCommandAsync(command);

        return result.IsSuccess ? Ok(result) : CreateErrorResponse(result.Error);
    }
    
    [HttpPost("CommitQuiz")]
    public async Task<IActionResult> CommitQuiz([FromBody] CommitQuizData commitQuizData)
    {
        var command = new CommitQuizCommand(commitQuizData.CourseId,
            commitQuizData.QuizId,
            commitQuizData.LearningId,
            commitQuizData.SolvedQuestions
                .Select(q => new SolvedQuestion
                {
                    Id = q.Id,
                    SolvedAnswers = q.SolvedAnswers
                        .Select(a => new SolvedAnswer()
                        {
                            Id = a.Id,
                            QuestionId = a.QuestionId,
                            IsSelected = a.IsSelected
                        })
                })
                .ToArray()
            );
        var result = await _coursesModule.ExecuteCommandAsync(command);

        return result.IsSuccess ? Ok(result) : CreateErrorResponse(result.Error);
    }
}