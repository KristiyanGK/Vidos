using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vidos.Data.Common
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();

        TEntity FindById(string id);

        void AttachRange(IEnumerable<object> entities);
    }
}