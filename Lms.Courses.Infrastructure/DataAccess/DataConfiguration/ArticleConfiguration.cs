using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Courses.Infrastructure.DataAccess.DataConfiguration;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Article");
        
        builder.Property(c => c.Id)
            .ValueGeneratedNever();
        
        builder.Property(c => c.Content)
            .HasConversion(t => t.Value,
                v => ArticleContent.Create(v));
    }
}

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Question");
        
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.HasMany(q => q.Answers)
            .WithOne()
            .HasForeignKey(q => q.QuestionId);
    }
}