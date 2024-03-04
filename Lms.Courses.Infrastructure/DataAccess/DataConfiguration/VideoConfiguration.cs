using Lms.Courses.Domain.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Courses.Infrastructure.DataAccess.DataConfiguration;

public class VideoConfiguration : IEntityTypeConfiguration<Video>
{
    public void Configure(EntityTypeBuilder<Video> builder)
    {
        builder.ToTable("Video");
        
        builder.Property(c => c.Id)
            .ValueGeneratedNever();
    }
}