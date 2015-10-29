using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Book
{
    public class BookListService
    {

        private IRepository<Book> Repository { get; set; }


        public BookListService(IRepository<Book> repository)
        {
            this.Repository = repository;
        }

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException();
            var foundBook = Repository.Get(b => book.Equals(b));
            if (foundBook == null)
                Repository.Add(book);
            else
                throw new Exception("This book exists in repository");
        }

        public void RemoveBook(Book book)
        {
            if (book==null)
                throw new ArgumentNullException();
            if (!Repository.Delete(book))
                throw new Exception("This book does not exist");
        }

        public Book FindByTag(Func<Book,bool> where)
        {
           return Repository.Get(where);
        }

        public IEnumerable<Book> SortBooksByTag(Func<Book,object> where)
        {
            return Repository.GetAll().OrderBy(where);
        }

    }
}
