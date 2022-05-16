using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;

namespace ElectronicJournal.Web.Repositories.Implementations
{
    public class CourseStatusRepository : BaseRepository<CourseStatus>, ICourseStatusRepository
    {
        public CourseStatusRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
