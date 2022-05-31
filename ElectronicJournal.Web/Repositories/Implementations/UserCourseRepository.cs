using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;
using System;
using System.Linq;

namespace ElectronicJournal.Web.Repositories.Implementations
{
    public class UserCourseRepository : BaseRepository<UserCourse>, IUserCourseRepository
    {
        public UserCourseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async void DeleteUser(Guid userId, Guid courseId)
        {
            var userCourse = await GetByCriteriaAsync(e => e.UserId == userId || courseId == courseId);

            await DeleteAsync(userCourse.FirstOrDefault().Id);
        }
    }

}
