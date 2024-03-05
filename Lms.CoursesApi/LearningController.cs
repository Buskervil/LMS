using Courses.Application.Core;
using Courses.Application.Learnings.AssignCourseToUser;
using Courses.Application.Learnings.CommitItem;
using Courses.Application.Learnings.StartLearnCourse;
using Lms.Core.Api;
using Lms.CoursesApi.Dto.Learning;
using Microsoft.AspNetCore.Mvc;

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
}