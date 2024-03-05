using Lms.Courses.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Courses.Infrastructure.DataAccess.DataConfiguration;

public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        builder.ToTable("Quiz");
        
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.HasMany(q => q.Questions)
            .WithOne()
            .HasForeignKey(q => q.QuizId);
    }
}