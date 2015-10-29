using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Book
{
    public interface IRepository<T> where T:class
    {
        void Add(T entity);
        bool Delete(T entity);
        int Delete(Predicate<Book> match);
        T Get(Func<T, bool> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Func<T, bool> where);

    }
}
