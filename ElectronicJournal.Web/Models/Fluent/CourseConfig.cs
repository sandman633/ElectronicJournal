using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicJournal.Web.Models.Fluent
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.Created)
                .IsRequired();

            builder.Property(x => x.Updated)
                .IsRequired(false);

            builder.HasOne(x => x.Teacher)
                .WithMany()
                .HasForeignKey(x=>x.TeacherId)
                .IsRequired(false);

        }
    }
}
