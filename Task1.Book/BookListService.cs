using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
namespace Task1.Book
{
    public class BookListService
    {

        private IRepository<Book> Repository { get; set; }
        private readonly Logger logger;

        public BookListService(IRepository<Book> repository)
        {
            this.Repository = repository;
            logger = LogManager.GetCurrentClassLogger();
        }

        public void AddBook(Book book)
        {
            logger.Trace("Starting AddBook method");
            if (book == null)
                throw new ArgumentNullException();
            var foundBook = Repository.Get(b => book.Equals(b));
            if (foundBook == null)
                Repository.Add(book);
            else
            {
                var e = new Exception("This book exists in repository");
                logger.Error(e, "Exception in AddBook method");
                throw e;
            }
            logger.Trace("AddBook method finished");
        }

        public void RemoveBook(Book book)
        {
            logger.Trace("Starting RemoveBook method");
            if (book == null)
            {
                var e = new ArgumentNullException();
                logger.Error(e, "Exception in RemoveBook method");
                throw e;
            }
            if (!Repository.Delete(book))
            {
                var e = new Exception("This book does not exist");
                logger.Error(e, "Exception in RemoveBook method");
                throw e;
            }
            logger.Trace("RemoveBook method finished");
        }

        public Book FindByTag(Func<Book, bool> where)
        {
            logger.Trace("Starting FindByTag method");
            return Repository.Get(where);
          
        }

        public IEnumerable<Book> SortBooksByTag(Func<Book, object> where)
        {
            logger.Trace("Starting SortBooksByTa gmethod");
            return Repository.GetAll().OrderBy(where);
        }

    }
}
