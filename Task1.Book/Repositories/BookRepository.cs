using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Book
{
    public class BookRepository:IRepository<Book>
    {
        string path;
        public BookRepository(string filePath)
        {
            this.path = filePath;
        }

        public IEnumerable<Book> GetAll()
        {
            FileMode fileMode = File.Exists(path) ? FileMode.Open : FileMode.Create;
            var list = new List<Book>();

            using (var br = new BinaryReader(new FileStream(path,fileMode)))
            {
                while(br.PeekChar() > -1)
                {
                    list.Add(new Book(br.ReadString(),br.ReadString(),br.ReadInt32(),br.ReadString()));
                }
            }
            return list;
        }

        public void Save(IEnumerable<Book> list)
        {
            FileMode fileMode = File.Exists(path) ? FileMode.Open : FileMode.Create;
            using (var br = new BinaryWriter(new FileStream(path, fileMode)))
            {
                foreach (var e in list)
                {
                    br.Write(e.Author);
                    br.Write(e.Title);
                    br.Write(e.Series);
                    br.Write(e.Language);
                }
            }
        }
    }
}
