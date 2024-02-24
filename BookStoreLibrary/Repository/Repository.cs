using BookStoreLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreLibrary.Repository
{
    public class Repository<T>:IRepository<T> where T : class
    {
        protected ShradhaBookStoresContext _dbcontext;
        public Repository(ShradhaBookStoresContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Add(T item)
        {
            _dbcontext.Set<T>().Add(item);
        }
        public void Delete (object id)
        {
            T entity = _dbcontext.Set<T>().Find(id);
            if (entity != null)
            {
                _dbcontext.Set<T>().Remove(entity);
            }
        }
        public T Find(object id) 
        {
            return _dbcontext.Set<T>().Find(id);
        }
        public IEnumerable<T> GetAll()
        {
            return _dbcontext.Set<T>().ToList();
        }
        public void Remove (T item)
        {
            _dbcontext.Set<T>().Remove(item);
        }
        public int SaveChange()
        {
            return _dbcontext.SaveChanges();
        }
        public void Update(T item)
        {
            _dbcontext.Set<T>().Update(item);
        }
    }

}
