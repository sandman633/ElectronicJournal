using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;

namespace ElectronicJournal.Web.Repositories.Implementations
{
    public class CourseTypeRepository : BaseRepository<CourseType>, ICourseTypeRepository
    {
        public CourseTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
