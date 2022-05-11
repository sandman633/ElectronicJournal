using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicJournal.Web.Models.Fluent
{
    public class CourseStatusConfig : IEntityTypeConfiguration<CourseStatus>
    {
        public void Configure(EntityTypeBuilder<CourseStatus> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(x => x.StatusName)
                .IsRequired();
        }
    }


}
