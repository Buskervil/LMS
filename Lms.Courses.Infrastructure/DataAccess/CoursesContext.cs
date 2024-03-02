using Lms.Courses.Domain.Course;
using Microsoft.EntityFrameworkCore;

namespace Lms.Courses.Infrastructure.DataAccess;

public class CoursesContext : DbContext
{
    public CoursesContext(DbContextOptions<CoursesContext> options)
    {
    }

    public DbSet<Course> Courses { get; private set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}