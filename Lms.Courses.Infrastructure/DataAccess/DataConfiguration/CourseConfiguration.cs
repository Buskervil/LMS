using Lms.Courses.Domain.Course;
using Lms.Courses.Domain.Course.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Courses.Infrastructure.DataAccess.DataConfiguration;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(t => t.Value,
                v => CourseId.Create(v));

        builder.Property(c => c.Name)
            .HasConversion(t => t.Value,
                v => EntityName.Create(v));
        
        builder.Property(c => c.Description)
            .HasConversion(t => t.Value,
                v => EntityDescription.Create(v));

        builder.HasMany(c => c.CourseSections)
            .WithOne()
            .HasForeignKey(s => s.CourseId);
    }
}