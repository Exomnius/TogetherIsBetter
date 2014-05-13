using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TogetherIsBetter.Model
{


    public class Generic<TEntity> where TEntity : class
    {
        private static TIB_Model db;
        private DbContext _context;


        public void Add(TEntity entity)
        {
            try
            {
                using (db = new TIB_Model())
                {

                    if (entity == null)
                    {
                        throw new ArgumentNullException("entity");
                    }

                    _context.Set<TEntity>().Add(entity);
                    _context.SaveChanges();

                }
            }
            catch(Exception ex){
                throw ex;
            }
        }

        public virtual void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

        }
    }
}
