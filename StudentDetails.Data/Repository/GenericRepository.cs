using StudentDetails.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace StudentDetails.Data.Repository
{
    public class GenericRepository<T> where T : class
    {
        private StudentDetailsDbContext _dbContext;
        private readonly IDbSet<T> _dbSet;

        public IDatabaseFactory DatabaseFactory { get; private set; }

        public StudentDetailsDbContext DbContext
        {
            get
            {
                return _dbContext ?? (_dbContext = DatabaseFactory.GetDbContext());
            }
        }

        public GenericRepository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            _dbSet = DbContext.Set<T>();
        }

        public T Single(object primaryKey)
        {
            return _dbSet.Find(primaryKey);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);

        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public virtual bool Exists(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).Any();
        }
    }
}
