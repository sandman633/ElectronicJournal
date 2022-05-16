using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;

namespace ElectronicJournal.Web.Repositories.Implementations
{
    public class CourseRequestRepository : BaseRepository<CourseRequest>, ICourseRequestRepository
    {
        public CourseRequestRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

}
