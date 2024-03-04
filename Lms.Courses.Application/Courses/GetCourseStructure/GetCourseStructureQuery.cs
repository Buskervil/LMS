using Courses.Application.Core;
using Courses.Application.Courses.GetCourseStructure.Dto;

namespace Courses.Application.Courses.GetCourseStructure;

public sealed class GetCourseStructureQuery : IQuery<CourseStructure>
{
    public Guid CourseId { get; }

    public GetCourseStructureQuery(Guid courseId)
    {
        CourseId = courseId;
    }
}