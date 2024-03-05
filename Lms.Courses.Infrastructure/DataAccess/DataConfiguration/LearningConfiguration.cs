using Lms.Courses.Domain.Learnings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lms.Courses.Infrastructure.DataAccess.DataConfiguration;

public class LearningConfiguration : IEntityTypeConfiguration<Learning>
{
    public void Configure(EntityTypeBuilder<Learning> builder)
    {
        builder.ToTable("Learning");
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.HasMany(l => l.Progresses)
            .WithOne()
            .HasForeignKey(p => p.LearningId);
    }
}