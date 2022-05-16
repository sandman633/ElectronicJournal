using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElectronicJournal.Web.Models.Fluent
{
    public class RequestStatusConfig : IEntityTypeConfiguration<RequestStatus>
    {
        public void Configure(EntityTypeBuilder<RequestStatus> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name);


            builder.HasMany(e => e.CourseRequests)
                .WithOne(e => e.RequestStatus)
                .HasForeignKey(e => e.RequestStatusId).HasPrincipalKey(e => e.RequestStatusId);
        }
    }
}
