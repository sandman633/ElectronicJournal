using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;

namespace ElectronicJournal.Web.Repositories.Implementations
{
    public class GroupRepository : BaseRepository<Group>, IGroupRepository
    {
        public GroupRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
