using Lms.Users.Domain.Employees;
using Lms.Users.Domain.Sessions;
using Microsoft.EntityFrameworkCore;

namespace Lms.Users.Infrastructure.DataAccess;

public class UsersContext : DbContext
{
    public UsersContext(DbContextOptions<UsersContext> options) : base(options)
    {
    }

    public DbSet<Session> Sessions { get; private set; } = null!;
    public DbSet<AuthenticationLogin> AuthenticationLogins { get; private set; }= null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}