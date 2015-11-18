using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Book;
using Task1.Book.Repositories;
namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = "Books.txt";
            var serialize_path = "Books_serialize.txt";
            var xml_path = "Books_xml.xml";
            var l2xml_path = "Books_l2xml.xml";
           // IRepository<Book> rep = new BookRepository(path);
           // IRepository<Book> rep = new SerializeRepository(serialize_path);
            //IRepository<Book> rep = new XMLRepository(xml_path);
            IRepository<Book> rep = new Linq2XmlRepository(l2xml_path);
            var bookService = new BookListService(rep);
            Book[] books =  { new Book("Albahari",@"C# in Nutshell",1,"ru"),                         
                             new Book("Esposito",@"ASP .NET MVC 4",1,"ru"),
                                 new Book("Albahari",@"C# in Nutshell",1,"en"),
                             new Book("Фримен",@"Паттерны проектирования",1,"ru")
                            };
            foreach (var e in books)
            {
                bookService.AddBook(e);
            }
            Console.WriteLine("Test Exception on adding");
            var b = new Book("Esposito", @"ASP .NET MVC 4", 1, "ru");
            try
            {
                bookService.AddBook(b);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Test finding the book");
            var f = bookService.FindByTag(x => x.Author == "Esposito");
            Console.WriteLine(f);

            Console.WriteLine("Test sorting");
            var books_1 = bookService.SortBooksByTag(x => x.Author);
            foreach (var e in books_1)
            {
                Console.WriteLine(e);
            }

            bookService.RemoveBook(f);
            try {
                bookService.RemoveBook(f); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}
