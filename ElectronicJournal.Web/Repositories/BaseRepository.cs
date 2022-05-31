using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces.CRUD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ElectronicJournal.Web.Repositories
{
    public class BaseRepository<TModel> : ICrudRepository<TModel>
            where TModel : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<TModel> DbSet => _context.Set<TModel>();

        /// <summary>
        /// Initializes the instance <see cref="BaseRepository{TModel, TEntity}"/>.
        /// </summary>
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronous method for creating entity.
        /// </summary>
        /// <param name="model">model.</param>
        /// <returns>ID of the created entity.</returns>
        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            await DbSet.AddAsync(model);
            _context.SaveChanges();

            return await GetByIdAsync(model.Id);
        }

        /// <summary>
        /// Asynchronous method for deleting entity.
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>A task that represents an asynchronous operation.</returns>
        public virtual async Task DeleteAsync(params Guid[] ids)
        {
            using (_context)
            {
                var entities = await DbSet.Where(x => ids.Contains(x.Id)).ToListAsync();
                DbSet.RemoveRange(entities);

                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Asynchronous method for getting entity.
        /// </summary>
        /// <returns>model collection.</returns>
        public virtual async Task<IEnumerable<TModel>> GetAsync()
        {
            var entity = await DefaultIncludeProperties(DbSet).AsNoTracking().ToListAsync();

            return entity;
        }

        /// <summary>
        /// Returns all entities that match the given filter.
        /// </summary>
        /// <param name="filter">Search criteria.</param>
        public virtual async Task<IEnumerable<TModel>> GetByCriteriaAsync(Expression<Func<TModel, bool>> filter = null)
        {
            IQueryable<TModel> entities = DefaultIncludeProperties(DbSet).AsNoTracking();
            if (filter != null)
                entities = entities.Where(filter);

            return entities;
        }

        /// <summary>
        /// Asynchronous method for getting entity by id.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <returns>model.</returns>
        public virtual async Task<TModel> GetByIdAsync(Guid? id)
        {
            var entities = await DefaultIncludeProperties(DbSet)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entities;
        }

        /// <summary>
        /// Asynchronous method for updating entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>model.</returns>
        public virtual async Task<TModel> UpdateAsync(TModel model)
        {
            DbSet.Update(model);
            _context.SaveChanges();

            var newEntity = await GetByIdAsync(model.Id);


            return newEntity;
        }
        /// <summary>
        /// Method for loading related data.
        /// </summary>
        /// <param name="dbSet">DbSet.</param>
        /// <returns>DbSet.</returns>
        protected virtual IQueryable<TModel> DefaultIncludeProperties(DbSet<TModel> dbSet) => dbSet;


    }
}
