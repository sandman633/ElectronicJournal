using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicJournal.Web.Models.Fluent
{
    public class PrincipalConfig : IEntityTypeConfiguration<Principal>
    {
        public void Configure(EntityTypeBuilder<Principal> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasMany(e => e.UsersPrincipal)
                .WithOne(e => e.Principal)
                .HasForeignKey(e => e.PrincipalId);

        }
    }

}
