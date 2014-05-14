using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TogetherIsBetter.Model
{
    public class Generic<TEntity> where TEntity : class
    {
        protected DbContext _context;

        public Generic()
        {
            _context = new TIB_Model();
        }

        public ICollection<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }


        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return _context.Set<TEntity>().SingleOrDefault(match);
        }


        public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return _context.Set<TEntity>().Where(match).ToList();
        }

        public TEntity Add(TEntity t)
        {
            _context.Set<TEntity>().Add(t);
            _context.SaveChanges();
            return t;
        }

        public TEntity Update(TEntity updated, int key)
        {
            if (updated == null)
                return null;

            TEntity existing = _context.Set<TEntity>().Find(key);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(updated);
                _context.SaveChanges();
            }
            return existing;
        }

        public void Delete(TEntity t)
        {
            _context.Set<TEntity>().Attach(t);
            _context.Set<TEntity>().Remove(t);
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
