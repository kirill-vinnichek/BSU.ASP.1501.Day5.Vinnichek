using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Book.Repositories
{
    public class SerializeRepository:IRepository<Book>
    {
        string path;


        public SerializeRepository(string filePath)
        {
            this.path = filePath;
        }
       
        public IEnumerable<Book> GetAll()
        {
            List<Book> list;
                var formatter = new BinaryFormatter();
                try
                {
                    using (var s = new FileStream(path, FileMode.Open))
                    {
                        list = (List<Book>)formatter.Deserialize(s);
                    }
                     return list;
                }
                catch (FileNotFoundException)
                {
                    return new List<Book>();
                }
                catch (SerializationException)
                {
                    return new List<Book>();
                }
      
        }
        public void Save(IEnumerable<Book> items)
        {
            FileMode fileMode = File.Exists(path) ? FileMode.Open : FileMode.Create;
            var formatter = new BinaryFormatter();
            using (var s = new FileStream(path, fileMode))
            {
              formatter.Serialize(s,items);
            }

        }
    }
}
