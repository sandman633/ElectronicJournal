using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;

namespace ElectronicJournal.Web.Repositories.Implementations
{
    public class UserCourseRepository : BaseRepository<UserCourse>, IUserCourseRepository
    {
        public UserCourseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

}
