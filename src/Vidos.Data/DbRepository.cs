using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidos.Data.Common;

namespace Vidos.Data
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class 
    {
        private readonly VidosContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public DbRepository(VidosContext context)
        {
            this._context = context;
            this._dbSet = this._context.Set<TEntity>();
        }

        public Task AddAsync(TEntity entity)
        {
            return this._dbSet.AddAsync(entity);
        }

        public IQueryable<TEntity> All()
        {
            return this._dbSet;
        }

        public void Delete(TEntity entity)
        {
            this._dbSet.Remove(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return this._context.SaveChangesAsync();
        }

        public TEntity FindById(string id)
        {
            return this._dbSet.Find(id);
        }

        public void AttachRange(IEnumerable<object> entities)
        {
            this._context.AttachRange(entities);
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
