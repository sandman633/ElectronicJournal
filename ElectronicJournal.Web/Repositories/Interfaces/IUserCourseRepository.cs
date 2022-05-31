using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces.CRUD;
using System;

namespace ElectronicJournal.Web.Repositories.Interfaces
{
    public interface IUserCourseRepository : ICrudRepository<UserCourse>
    {
        void DeleteUser(Guid userId, Guid courseId);
    }
}
