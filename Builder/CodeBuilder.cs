using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public enum Modifier
    {
        Private,
        Protected,
        Internal,
        Public
    }

    public class Field
    {
        public Modifier FieldMofidier { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{FieldMofidier.ToString().ToLower()} {Type.ToLower()} {Name};";
        }
    }

    public class Class
    {
        public string Name { get; set; }
        public Modifier ClassModifier { get; set; }

        public override string ToString()
        {
            return $"{ClassModifier.ToString().ToLower()} class {Name}";
        }
    }

    public class CodeBuilder
    {
        protected Class ClassInfo { get; set; }

        protected List<Field> Fields = new List<Field>();

        public CodeBuilder(string className)
        {
            var modifier = Modifier.Public;
            ClassInfo = new Class() { Name = className, ClassModifier = modifier };
        }

        public CodeBuilder AddField(string fieldName, string type)
        {
            Fields.Add(new Field() { Name = fieldName, Type = type });
            return this;
        }

        public override string ToString()
        {
            var indent = new string(' ', 2);
            var sb = new StringBuilder();

            sb.AppendLine(ClassInfo.ToString());
            sb.AppendLine("{");
            foreach(var field in Fields)
            {
                sb.AppendLine(indent+field.ToString());
            }
            sb.AppendLine("}");

            return sb.ToString();
        }

        public static void MainFunc(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb.ToString());
        }
    }
}
