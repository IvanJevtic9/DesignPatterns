using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator
{
    /* Scenario
        var s = "Hello "
        s += "world" 
        -------> Kreiranje novog stringa spajanjem ova dva

        StringBuilder s = "Hello "
        s += "world"   
        -------> Nece proci
    */
    public class MyStringBuilder
    {
        private StringBuilder sb = new StringBuilder();

        public static implicit operator MyStringBuilder(string s)
        {
            var msb = new MyStringBuilder();
            msb.sb.Append(s);

            return msb;
        }

        public static implicit operator string (MyStringBuilder b)
        {
           return b.ToString();
        }

        public MyStringBuilder Append(string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                sb.Append(value);
            }
            return this;
        }
        public MyStringBuilder AppendLine(string? value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                sb.AppendLine(value);
            }
            return this;
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }

    public class AdapterDecorator
    {
        public static void MainFunc(string[] args)
        {
            var b = new MyStringBuilder();

            b += "Hello";
            b.Append(" world");

            string p = "#---> " + b;

            Console.WriteLine((string)b);
            Console.WriteLine(p);
        }
    }
}
