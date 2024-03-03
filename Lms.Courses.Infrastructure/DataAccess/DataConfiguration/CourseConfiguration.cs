using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;
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

        builder.Property(c => c.Duration)
            .HasConversion(t => t.TotalDays, 
                v => TimeSpan.FromDays(v));
        
        builder.Ignore(c => c.Items);

        builder.HasMany(c => c.CourseSections)
            .WithOne()
            .HasForeignKey(s => s.CourseId);
    }
}