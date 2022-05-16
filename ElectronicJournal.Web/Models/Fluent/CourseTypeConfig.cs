using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicJournal.Web.Models.Fluent
{
    public class CourseTypeConfig : IEntityTypeConfiguration<CourseType>
    {
        public void Configure(EntityTypeBuilder<CourseType> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name);

            builder.HasMany(e => e.Courses)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId).HasPrincipalKey(e => e.TypeId);

            builder.HasMany(e => e.CourseRequests)
                .WithOne(e => e.Type)
                .HasForeignKey(e => e.TypeId).HasPrincipalKey(e => e.TypeId);
        }
    }
}
