using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZipPay.Domain.Interface.Repository.Common;
using ZipPay.Infrastructure.Data;

namespace ZipPay.Infrastructure.Persistence.Repository.Common
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ZipPayDataContext _dbContext;
        private DbSet<TEntity> table = null;
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public GenericRepository(ZipPayDataContext dbContext)
        {
            this._dbContext = dbContext;
            table = _dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).AsNoTracking().ToListAsync();
            }
            else
            {
                return await query.AsNoTracking().ToListAsync();
            }

        }
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await table.FindAsync(id);
        }
        public async Task InsertAsync(TEntity obj)
        {
            await table.AddAsync(obj);
        }
        public async Task InsertRangeAsync(IEnumerable<TEntity> objEnumerable)
        {
            await table.AddRangeAsync(objEnumerable);
        }
        public void InsertRange(IEnumerable<TEntity> objEnumerable)
        {
            table.AddRange(objEnumerable);
        }
        public void Update(TEntity obj)
        {
            table.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            TEntity existing = table.Find(id);
            table.Remove(existing);
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).AsNoTracking().ToList();
            }
            else
            {
                return query.AsNoTracking().ToList();
            }
        }

        public TEntity GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(TEntity obj)
        {
            table.Add(obj);
        }


    }
}
