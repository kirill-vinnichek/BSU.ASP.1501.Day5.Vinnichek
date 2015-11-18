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
        private List<Book> list;
        public BookListService(IRepository<Book> repository)
        {
            this.Repository = repository;
            logger = LogManager.GetCurrentClassLogger();
            try
            {
                list = repository.GetAll().ToList();
            }
           finally
            {
                list = new List<Book>();
            }

        }

        public void AddBook(Book book)
        {
            logger.Trace("Starting AddBook method");
            if (book == null)
                throw new ArgumentNullException();
            var foundBook = list.FirstOrDefault(b => book.Equals(b));
            if (foundBook == null)
            {
                list.Add(book);
                Repository.Save(list);
            }
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
            if (!list.Remove(book))
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
            return list.FirstOrDefault(where);

        }

        public IEnumerable<Book> SortBooksByTag(Func<Book, object> where)
        {
            logger.Trace("Starting SortBooksByTa gmethod");
            return Repository.GetAll().OrderBy(where);
        }

    }
}
