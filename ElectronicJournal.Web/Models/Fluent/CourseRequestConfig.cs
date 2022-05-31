using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace ElectronicJournal.Web.Models.Fluent
{
    public class CourseRequestConfig : IEntityTypeConfiguration<CourseRequest>
    {
        public void Configure(EntityTypeBuilder<CourseRequest> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Reason)
                .IsRequired();
            builder.Property(x => x.Created)
                .IsRequired();
            builder.Property(x => x.Updated)
                .IsRequired(false);
            builder.HasOne(x => x.Principal)
                .WithMany(x=>x.UserCourseRequestsPrincipal)
                .HasForeignKey(x => x.PrincipalId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Sender)
                .WithMany(x=>x.UserCourseRequestsSender)
                .HasForeignKey(x => x.SenderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
