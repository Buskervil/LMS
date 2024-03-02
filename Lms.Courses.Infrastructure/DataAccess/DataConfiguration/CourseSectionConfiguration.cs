using Lms.Courses.Domain.Course;
using Lms.Courses.Domain.Course.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Courses.Infrastructure.DataAccess.DataConfiguration;

public class CourseSectionConfiguration : IEntityTypeConfiguration<CourseSection>
{
    public void Configure(EntityTypeBuilder<CourseSection> builder)
    {
        builder.ToTable("CourseSection");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.Name)
            .HasConversion(t => t.Value,
                v => EntityName.Create(v));
        
        builder.Property(c => c.Description)
            .HasConversion(t => t.Value,
                v => EntityDescription.Create(v));

        builder.HasMany(c => c.CourseItems)
            .WithOne()
            .HasForeignKey(s => s.CourseSectionId);
    }
}