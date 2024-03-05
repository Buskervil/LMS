using Courses.Application.Core;
using Dapper;
using Lms.Core.Application;
using Lms.Core.Domain.Results;

namespace Courses.Application.Courses.GetCourses;

internal sealed class GetCoursesQueryHandler : IQueryHandler<GetCoursesQuery, IReadOnlyCollection<CourseData>>
{
    private ISqlConnectionFactory _connectionFactory;

    public GetCoursesQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Result<IReadOnlyCollection<CourseData>>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var connection = _connectionFactory.GetOpenConnection();
        var transaction = _connectionFactory.GetOpenTransaction();

        const string coursesQuery = """
            SELECT C."Id", "Name", "Description", "Duration", "CreatedAt",
                   
            CASE
            WHEN L."CourseId" IS NOT NULL THEN TRUE
            ELSE FALSE
            END AS "Started",
                
            CASE
            WHEN L."FinishedAt" IS NOT NULL THEN TRUE
            ELSE FALSE
            END AS "Ended"
            FROM "Course" C
            LEFT JOIN "Learning" L ON L."CourseId" = C."Id"
            """;

        var courses = await connection.QueryAsync<CourseData>(coursesQuery, transaction: transaction);

        return courses.ToArray();
    }
}