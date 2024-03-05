using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Learnings;
using Microsoft.EntityFrameworkCore;

namespace Lms.Courses.Infrastructure.DataAccess;

public class CoursesContext : DbContext, IUnitOfWork
{
    public CoursesContext(DbContextOptions<CoursesContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; private set; } = null!;
    public DbSet<Learning> Learnings { get; private set; } = null!;
    public DbSet<Quiz> Quizes { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return SaveChangesAsync(cancellationToken);
    }
}