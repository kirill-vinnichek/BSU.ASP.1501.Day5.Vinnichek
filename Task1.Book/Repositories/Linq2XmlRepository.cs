using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Task1.Book.Repositories
{
    public class Linq2XmlRepository : IRepository<Book>
    {
        string path;

        public Linq2XmlRepository(string filePath)
        {
            this.path = filePath;
        }

        public IEnumerable<Book> GetAll()
        {
            var list = new List<Book>();
            if (!File.Exists(path))
                return list;
            var doc = XDocument.Load(path);
            string lang, author, title;
            int series;
            foreach (var e in doc.Root.Elements("Book"))
            {

                lang = e.Attribute("Language").Value;
                author = e.Element("Author").Value;
                title = e.Element("Title").Value;
                series = Int32.Parse(e.Element("Series").Value);
                list.Add(new Book(author, title, series, lang));
            }
            return list;
        }

        public void Save(IEnumerable<Book> items)
        {
            var xml = new XElement("Books",
                items.Select((b) => new XElement("Book",
                    new XAttribute("Language",b.Language),
                    new XElement("Author",b.Author),
                    new XElement("Title",b.Title),
                    new XElement("Series",b.Series))));
            xml.Save(path);
        }
    }
}
