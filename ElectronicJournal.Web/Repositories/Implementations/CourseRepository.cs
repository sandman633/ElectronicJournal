using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ElectronicJournal.Web.Repositories.Implementations
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<Course> DefaultIncludeProperties(DbSet<Course> dbSet)
        {
            return dbSet.Include(e=>e.Type).Include(e=>e.Teacher).Include(e=>e.UserCourses);
        }
    }
}
