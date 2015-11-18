using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Book
{

    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        void Save(IEnumerable<T> items);

    }
}
