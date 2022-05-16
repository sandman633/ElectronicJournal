using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElectronicJournal.Web.Repositories.Interfaces.CRUD
{
    public interface IGettable<TModel>
    {
        Task<IEnumerable<TModel>> GetAsync();
        Task<IEnumerable<TModel>> GetByCriteriaAsync(Expression<Func<TModel, bool>> filter = null);
        Task<TModel> GetByIdAsync(Guid? id);
    }
}
