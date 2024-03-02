using Courses.Domain.Course;
using Courses.Domain.Course.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Courses.Infrastructure.DataAccess.DataConfiguration;

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

        builder.HasMany(c => c.CourseSections)
            .WithOne()
            .HasForeignKey(s => s.CourseId);
    }
}