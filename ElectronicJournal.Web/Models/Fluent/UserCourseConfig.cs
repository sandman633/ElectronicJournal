using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicJournal.Web.Models.Fluent
{
    public class UserCourseConfig : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Course)
                .WithMany(e=>e.UserCourses)
                .HasForeignKey(e => e.CourseId);

            builder.HasOne(e => e.User)
                .WithMany(e=>e.UserCourses)
                .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Status)
                .WithMany(e=>e.UserCourses)
                .HasForeignKey(e=>e.StatusId)
                .HasPrincipalKey(e=>e.StatusId);
        }
    }
}
