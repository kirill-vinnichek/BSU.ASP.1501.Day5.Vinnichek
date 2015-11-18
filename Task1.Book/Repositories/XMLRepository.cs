using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Task1.Book.Repositories
{
    public class XMLRepository : IRepository<Book>
    {
        string path;

        public XMLRepository(string filePath)
        {
            this.path = filePath;
        }

        public IEnumerable<Book> GetAll()
        {
            var list = new List<Book>();
            string lang = string.Empty, author = string.Empty, title = string.Empty;
            int series = 0;
            if (!File.Exists(path))
                return list;
            using (var reader = XmlReader.Create(path))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (reader.Name)
                            {
                                case "Book":
                                    lang = reader.GetAttribute("Language");
                                    break;
                                case "Author":
                                    author = reader.ReadElementContentAsString();
                                    break;
                                case "Title":
                                    title = reader.ReadElementContentAsString();
                                    break;
                                case "Series":
                                    series = reader.ReadElementContentAsInt();
                                    break;
                            }
                            break;
                        case XmlNodeType.EndElement:
                            if (reader.Name == "Book")
                                list.Add(new Book(author, title, series, lang));
                            break;
                    }
                }
                return list;
            }
        }


        public void Save(IEnumerable<Book> items)
        {

            var settings = new XmlWriterSettings() { Indent = true, ConformanceLevel = ConformanceLevel.Auto };
            using (var s = new FileStream(path, FileMode.Create))
            {
                using (var writer = XmlWriter.Create(s, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("Books");
                    foreach (var e in items)
                    {
                        writer.WriteStartElement("Book");
                        writer.WriteAttributeString("Language", e.Language);
                        writer.WriteElementString("Author", e.Author);
                        writer.WriteElementString("Title", e.Title);
                        writer.WriteStartElement("Series");
                        writer.WriteValue(e.Series);
                        writer.WriteEndElement();
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
        }
    }
}
