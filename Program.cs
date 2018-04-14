using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("ken grimwood", "sil baştan", 10);
            book.Display();
            

            var video = new Video("Spielberg", "Jaws", 100, 15);
            video.Display();

            Console.WriteLine("\nVideo yu ödünç verilebilir yapalım");
            var borrow = new Borrowable<Video>(video);

            borrow.BorrowItem(" #Customer 1");
            borrow.BorrowItem(" #Customer 2");

            borrow.Display();

            Console.Read();
        }
    }

    abstract class LibraryItem<T>
    {
        public static int NumCopies { get; set; }
        public abstract void Display();
    }

    class Book : LibraryItem<Book>
    {
        private string _author;
        private string _title;

        public Book(string a, string t, int numC)
        {
            _author = a;
            _title = t;
            NumCopies = numC;
        }
        public override void Display()
        {
            Console.WriteLine(" \nBook --------");
            Console.WriteLine(" Author: {0}", _author);
            Console.WriteLine(" Title: {0}", _title);
            Console.WriteLine(" # Copies: {0}", NumCopies);
        }
    }

    class Video : LibraryItem<Video>
    {
        private string _director;
        private string _title;
        private int _playTime;
        public Video(string d, string t, int pt, int numC)
        {
            _director = d;
            _title = t;
            NumCopies = numC;
            _playTime = pt;
        }
        public override void Display()
        {
            Console.WriteLine(" \nVideo --------");
            Console.WriteLine(" Director: {0}", _director);
            Console.WriteLine(" Title: {0}", _title);
            Console.WriteLine(" PLay Time: {0}", _playTime);
            Console.WriteLine(" # Copies: {0}", NumCopies);
        }
    }

    abstract class Decorator<T> : LibraryItem<T>
    {
        private LibraryItem<T> _libraryItem;

        public Decorator(LibraryItem<T> libraryItem)
        {
            _libraryItem = libraryItem;
        }

        public override void Display()
        {
            _libraryItem.Display();
        }
    }

    class Borrowable<T> : Decorator<T>
    {
        private List<string> _borrowers = new List<string>();
        public Borrowable(LibraryItem<T> libraryItem) : base(libraryItem)
        {
        }

        public void BorrowItem(string name)
        {
            _borrowers.Add(name);
            NumCopies--;
        }

        public void ReturnItem(string name)
        {
            _borrowers.Remove(name);
            NumCopies++;
        }

        public override void Display()
        {
            base.Display();
            _borrowers.ForEach(b => Console.WriteLine(" borrower:" + b));
        }
    }
}
