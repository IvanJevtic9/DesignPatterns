using System;
using System.Collections.Generic;
using System.Text;

namespace Decorator
{
    public class CodeBuilder
    {
        private StringBuilder stringBuilder;
        public CodeBuilder()
        {
            stringBuilder = new StringBuilder();
        }

        // Overriding StringBuilder + make it like fluent api
        public CodeBuilder Append(string value)
        {
            stringBuilder.Append(value);
            return this;
        }

        public CodeBuilder AppendLine(string value)
        {
            stringBuilder.AppendLine(value);
            return this;
        }

        public override string ToString()
        {
            return stringBuilder.ToString();
        }
    }

    public class CustomStringBuilder
    {
        public static void MainFunc(string[] args)
        {
            var codeBuilder = new CodeBuilder();

            codeBuilder.AppendLine("Ivan Jevtic")
                       .AppendLine("Software developer")
                       .Append("--------------------");

            Console.WriteLine(codeBuilder.ToString());
        }
    }
}
