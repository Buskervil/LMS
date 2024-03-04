using Lms.Users.Domain.Employees;
using Lms.Users.Domain.Employees.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Users.Infrastructure.DataAccess.DataConfiguration;

public class AuthenticationLoginConfiguration : IEntityTypeConfiguration<AuthenticationLogin>
{
    public void Configure(EntityTypeBuilder<AuthenticationLogin> builder)
    {
        builder.ToTable("AuthenticationLogin");
        builder.HasKey(c => c.Login);

        builder.Property(c => c.Login)
            .ValueGeneratedNever()
            .HasConversion(t => t.Value,
                v => Login.Create(v));

        builder.Property(a => a.Password)
            .HasConversion(t => t.Value,
                v => new Password(v));
    }
}