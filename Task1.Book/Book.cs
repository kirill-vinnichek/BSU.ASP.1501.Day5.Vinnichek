using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Book
{
    [Serializable]
    public class Book : IEquatable<Book>, IComparable<Book>
    {

        public string Author { get; set; }

        public string Title { get; set; }

        public int Series { get; set; }

        public string Language { get; set; }


        public Book(string author,string title,int series,string lang)
        {
            this.Author = author;
            this.Title = title;
            this.Series = series;
            this.Language = lang;
        }


        public bool Equals(Book other)
        {
            if (other == null)
                return false;
            if(ReferenceEquals(this,other))
                return true;
            if (this.CompareTo(other)==0)
                return true;
            return false;
        }

        public int CompareTo(Book other)
        {
            int authorCompare = this.Author.ToUpperInvariant().CompareTo(other.Author.ToUpperInvariant());
            if (authorCompare != 0)
                return authorCompare;
            int titleCompare = this.Title.ToUpperInvariant().CompareTo(other.Title.ToUpperInvariant());
            if (titleCompare != 0)
                return titleCompare;
            int langCompare = this.Language.ToUpperInvariant().CompareTo(other.Language.ToUpperInvariant());
            if (langCompare != 0)
                return langCompare;
            return this.Series.CompareTo(other.Series);
        }

        public override string ToString()
        {
            return String.Format("{0} - {1} ({2})",this.Author,this.Title,this.Language);
        }
    }
}
