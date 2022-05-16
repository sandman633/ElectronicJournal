using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ElectronicJournal.Web.Repositories.Implementations
{
    public class UserGroupRepository : BaseRepository<UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(ApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<UserGroup> DefaultIncludeProperties(DbSet<UserGroup> dbSet)
        {
            return dbSet.Include(e=>e.Group).Include(e=>e.User);
        }
    }
}
