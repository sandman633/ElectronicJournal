using System;
using System.Threading.Tasks;

namespace ElectronicJournal.Web.Repositories.Interfaces.CRUD
{
    public interface IDeletable
    {
        Task DeleteAsync(params Guid[] ids);
    }
}
