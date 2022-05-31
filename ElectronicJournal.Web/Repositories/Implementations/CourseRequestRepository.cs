using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ElectronicJournal.Web.Repositories.Implementations
{
    public class CourseRequestRepository : BaseRepository<CourseRequest>, ICourseRequestRepository
    {
        public CourseRequestRepository(ApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<CourseRequest> DefaultIncludeProperties(DbSet<CourseRequest> dbSet)
        {
            return dbSet.Include(e => e.Sender).Include(e => e.Principal).Include(e => e.RequestStatus).Include(e => e.Course).ThenInclude(e => e.Type);
        }

    }

}
