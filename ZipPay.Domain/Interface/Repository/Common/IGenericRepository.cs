using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZipPay.Domain.Interface.Repository.Common
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        void Delete(object id);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);
        void Insert(TEntity obj);
        void InsertRange(IEnumerable<TEntity> objEnumerable);
        Task InsertRangeAsync(IEnumerable<TEntity> objEnumerable);
        Task InsertAsync(TEntity obj);
        void Save();
        Task SaveAsync();
        void Update(TEntity obj);


    }
}
