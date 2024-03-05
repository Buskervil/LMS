using Courses.Application.Core;
using Courses.Application.Statistics.GetCourseStatistics;
using Lms.Core.Api;
using Microsoft.AspNetCore.Mvc;

namespace Lms.CoursesApi;

[Route("api/statistics")]
public class StatisticsController : BaseController
{
    private readonly ICoursesModule _coursesModule;

    public StatisticsController(ICoursesModule coursesModule)
    {
        _coursesModule = coursesModule;
    }

    [HttpGet("bycourse/{courseId}")]
    public async Task<IActionResult> GetStatisticsByCourse(Guid courseId)
    {
        var query = new GetCourseStatisticsQuery { CourseId = courseId };
        var result = await _coursesModule.ExecuteQueryAsync(query);
        
        return result.IsSuccess ? Ok(result) : CreateErrorResponse(result.Error);

    }
}