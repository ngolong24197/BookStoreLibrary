using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreLibrary.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Find(object id);

       
        void Add (T entity);
        void Update (T entity);
        void Remove (T entity);
        void Delete (object id);
        int SaveChange();

    }
}
