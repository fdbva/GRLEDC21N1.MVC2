using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CrudRepository<TModel> : ICrudRepository<TModel>
        where TModel : BaseModel
    {
        private readonly BibliotecaContext _bibliotecaContext;
        private readonly DbSet<TModel> _dbSet;

        public CrudRepository(
            BibliotecaContext bibliotecaContext)
        {
            _bibliotecaContext = bibliotecaContext;

            _dbSet = _bibliotecaContext.Set<TModel>();
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return _dbSet;
        }

        public virtual async Task<TModel> GetByIdAsync(int id)
        {
            var model = await _dbSet
                .FirstOrDefaultAsync(x => x.Id == id);

            return model;
        }

        public virtual async Task<TModel> CreateAsync(TModel model)
        {
            var modelCreated = _dbSet.Add(model);

            return modelCreated.Entity;
        }

        public virtual async Task<TModel> EditAsync(TModel model)
        {
            var modelEdited = _dbSet.Update(model);

            return modelEdited.Entity;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var model = await GetByIdAsync(id);

            _dbSet.Remove(model);
        }
    }
}
