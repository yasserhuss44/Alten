using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Helpers.Generics
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _dataBaseContext;

        public DbSet<T> DbSet => _dataBaseContext.Set<T>();

        public GenericRepository(DbContext context)
        {
            _dataBaseContext = context;
        }

        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = _dataBaseContext.Set<T>();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty).AsQueryable();
                }

                if (orderBy != null)
                {
                    return orderBy(query);
                }

                return query;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual T FindSingle(int id)
        {
            return _dataBaseContext.Set<T>().Find(id);
        }

        public virtual T FindBy(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = _dataBaseContext.Set<T>();
            foreach (string includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.Where(predicate).FirstOrDefault();
        }

        public virtual void Add(T toAdd)
        {
            _dataBaseContext.Set<T>().Add(toAdd);

        }

        public virtual void Update(T toUpdate)
        {
            _dataBaseContext.Entry(toUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(int id)
        {
            T entity = FindSingle(id);
            _dataBaseContext.Set<T>().Remove(entity);
        }

        public virtual void Delete(T entity)
        {
            _dataBaseContext.Set<T>().Remove(entity);
        }

        public virtual void Save()
        {
            _dataBaseContext.SaveChanges();
        }
    }
}
