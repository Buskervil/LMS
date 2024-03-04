using Courses.Application.Core;
using Courses.Application.Courses.AddArticle;
using Courses.Application.Courses.AddSection;
using Courses.Application.Courses.CreateCourse;
using Lms.Core.Api;
using Lms.CoursesApi.Dto;
using Lms.CoursesApi.Dto.Courses;
using Microsoft.AspNetCore.Mvc;

namespace Lms.CoursesApi;

[Route("api/course")]
public class CourseController : BaseController
{
    private readonly ICoursesModule _coursesModule;

    public CourseController(ICoursesModule coursesModule)
    {
        _coursesModule = coursesModule;
    }

    [HttpPost("")]
    [ServiceFilter(typeof(AuthorizeFilter))]
    public async Task<IActionResult> CreateCourse(CourseCreateData createData)
    {
        var command = new CreateCourseCommand(createData.Name, createData.Description);
        var result = await _coursesModule.ExecuteCommandAsync(command);

        return result.IsFailure ? CreateErrorResponse(result.Error) : Ok(result);
    }

    [HttpPost("AddSection")]
    public async Task<IActionResult> AddSection(SectionCreateData createData)
    {
        var command = new AddSectionCommand(createData.Name, createData.Description, createData.CourseId, createData.PreviousSectionId);
        var result = await _coursesModule.ExecuteCommandAsync(command);

        return result.IsFailure ? CreateErrorResponse(result.Error) : Ok(result);
    }

    [HttpPost("AddArticle")]
    public async Task<IActionResult> AddArticle(ArticleCreateData createData)
    {
        var command = new AddArticleCommand(createData.Name,
            createData.SectionId,
            createData.CourseId,
            createData.PreviousSectionId,
            createData.Content);
        var result = await _coursesModule.ExecuteCommandAsync(command);

        return result.IsFailure ? CreateErrorResponse(result.Error) : Ok(result);
    }
}