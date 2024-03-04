using Lms.Courses.Domain.Courses;
using Lms.Courses.Domain.Courses.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Courses.Infrastructure.DataAccess.DataConfiguration;

public class CourseItemConfiguration : IEntityTypeConfiguration<CourseItem>
{
    public void Configure(EntityTypeBuilder<CourseItem> builder)
    {
        builder.UseTpcMappingStrategy();
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .HasConversion(t => t.Value,
                v => EntityName.Create(v));
    }
}