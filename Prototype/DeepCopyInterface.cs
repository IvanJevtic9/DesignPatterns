using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }

    public class Student : IPrototype<Student>
    {
        public Booklet Booklet;
        public Student(string number)
        {
            Booklet = new Booklet(number);
        }
        public Student DeepCopy()
        {
            return new Student(Booklet.IndexNumber);
        }
        public override string ToString()
        {
            return $"Index: {Booklet.IndexNumber}";
        }
    }

    public class Booklet : IPrototype<Booklet>
    {
        public string IndexNumber;
        public Booklet(string num)
        {
            IndexNumber = num;
        }
        public Booklet DeepCopy()
        {
            return new Booklet(IndexNumber);
        }
    }

    public class DeepCopyInterface
    {
        public static void MainFunc(string[] args)
        {
            var st = new Student("135/2020");

            var st2 = st.DeepCopy();
            st2.Booklet.IndexNumber = "10/2020";

            Console.WriteLine(st);
            Console.WriteLine(st2);
        }
    }
}
