using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TimeHelper.Data.DbAccess.Repositories.Base
{
    public abstract class EFRepositoryBase<T> where T : class
    {
        #region Fields

        private readonly DbContext _dbContext;
        protected DbSet<T> _dbSet;

        #endregion

        #region Constructor

        protected EFRepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        #endregion

        #region Public methods

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Add(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> collection)
        {
            _dbSet.RemoveRange(collection);
        }

        public void Delete(string id)
        {
            var entity = _dbSet.Find(id);

            if (entity != null)
                _dbSet.Remove(entity);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking().ToList();
        }

        public IQueryable<T> GetAsQueryable(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _dbSet.AsNoTracking();
        }

        public T GetById(string id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        #endregion
    }
}
