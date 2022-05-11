using System.Threading.Tasks;

namespace ElectronicJournal.Web.Repositories.Interfaces.CRUD
{
    public interface IUpdatable<TModel>
    {
        Task<TModel> UpdateAsync(TModel model);
    }
}
