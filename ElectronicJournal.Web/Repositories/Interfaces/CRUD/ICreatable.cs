using System.Threading.Tasks;

namespace ElectronicJournal.Web.Repositories.Interfaces.CRUD
{
    public interface ICreatable<TModel>
    {
        Task<TModel> CreateAsync(TModel model);
    }
}
