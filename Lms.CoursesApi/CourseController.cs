using Courses.Application.Core;
using Courses.Application.Courses.AddArticle;
using Courses.Application.Courses.AddSection;
using Courses.Application.Courses.CreateCourse;
using Courses.Application.Courses.GetCourses;
using Courses.Application.Courses.GetCourseStructure;
using Courses.Application.Courses.GetItem;
using Lms.Core.Api;
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

    [HttpGet]
    //[ServiceFilter(typeof(AuthorizeFilter))]
    public async Task<IActionResult> GetCourses()
    {
        var query = new GetCoursesQuery();
        var result = await _coursesModule.ExecuteQueryAsync(query);

        return result.IsFailure ? CreateErrorResponse(result.Error) : Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(Guid id)
    {
        var query = new GetCourseStructureQuery(id);
        var result = await _coursesModule.ExecuteQueryAsync(query);

        return result.IsFailure ? CreateErrorResponse(result.Error) : Ok(result.Value);
    }
    
    [HttpGet("{courseId}/item/{itemId}")]
    public async Task<IActionResult> GetItem(Guid courseId, Guid itemId)
    {
        var query = new GetItemQuery(courseId, itemId);
        var result = await _coursesModule.ExecuteQueryAsync(query);

        return result.IsFailure ? CreateErrorResponse(result.Error) : Ok(result.Value);
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

        return result.IsFailure ? CreateErrorResponse(result.Error) : Ok(result.Value);
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

        return result.IsFailure ? CreateErrorResponse(result.Error) : Ok(result.Value);
    }
}