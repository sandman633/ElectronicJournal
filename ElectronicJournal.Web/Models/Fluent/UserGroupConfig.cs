using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicJournal.Web.Models.Fluent
{
    public class UserGroupConfig : IEntityTypeConfiguration<UserGroup>
    {
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Group)
                .WithMany(e=>e.UserGroups)
                .HasForeignKey(e => e.GroupId);

            builder.HasOne(e => e.User)
                .WithMany(e => e.UserGroups)
                .HasForeignKey(e => e.UserId);
        }
    }


}
