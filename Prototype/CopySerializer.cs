using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Prototype
{
    public static class ExtensionMethod
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();

            // Pise se self u memory stream, da bi self mogao da se searilizuje mora se navesti [Serializable]
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);

            object copyObj = formatter.Deserialize(stream);
            stream.Close();

            return (T)copyObj;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                // Ovaj XmlSerializer zahteva da imamo paramless constructor onoga sto searilizujemo
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T)s.Deserialize(ms);
            }
        }
    }
    [Serializable] // Potrebno za Binary formater searilizer
    public class Book
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public double Price { get; set; }
        public Book(string title, Author author, double price)
        {
            Title = title;
            Author = author;
            Price = price;
        }
        public Book() // Potrebno za XmlSerializer
        {

        }
        public override string ToString()
        {
            return $"{nameof(Title)}: {Title}\n{nameof(Author)}:\n\t" +
                   $"{nameof(Author.Name)}: {Author.Name}\n\t" +
                   $"{nameof(Author.Surname)}: {Author.Surname}\n" +
                   $"{nameof(Price)}: {Price}";
        }
    }
    [Serializable] 
    public class Author
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Author(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public Author()
        {

        }
    }

    public class CopySerializer
    {
        public static void MainFunc(string[] args)
        {
            var book = new Book("Programiranje1", new Author("Predrag", "Janicic"), 1500);
            var book2 = ExtensionMethod.DeepCopy<Book>(book);
            var book3 = ExtensionMethod.DeepCopyXml<Book>(book);

            book2.Title = "Programiranje 2";
            book3.Title = "Vestacka inteligencija";
            book3.Price = 2000;

            Console.WriteLine(book.ToString());
            Console.WriteLine(book2.ToString());
            Console.WriteLine(book3.ToString());
        }
    }
}
