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

        public void Add(Book entity)
        {
            if (entity == null)
                throw new ArgumentNullException();
            using (var br = new BinaryWriter(new FileStream(path,FileMode.Append,FileAccess.Write)))
            {
                br.Write(entity.Author);
                br.Write(entity.Title);
                br.Write(entity.Series);
                br.Write(entity.Language);
            }
        }

        public bool Delete(Book entity)
        {
            if (Delete(b => entity.Equals(b)) != 0)
                return true;
            return  false;
        }

        public int Delete(Predicate<Book> match)
        {
            var list = GetAll().ToList();
            var result = list.RemoveAll(match);
            if (result!=0)
                CreateFile(list);
            return result;
        }

        public Book Get(Func<Book, bool> where)
        {
            return GetAll().FirstOrDefault(where);
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

        public IEnumerable<Book> GetMany(Func<Book, bool> where)
        {
            return GetAll().Where(where);
        }


        private void CreateFile(IEnumerable<Book> list)
        {
            using (var br = new BinaryWriter(new FileStream(path,FileMode.Create)))
            {
                foreach(var e in list)
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
