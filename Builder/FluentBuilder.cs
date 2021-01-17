using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public class HtmlElement
    {
        private const int identSize = 2;

        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();

        public HtmlElement()
        { }
        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        public string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', identSize * indent);

            sb.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.AppendLine(new string(' ', identSize * (indent + 1)) + $"{Text}");
            }

            foreach (var element in Elements)
            {
                sb.Append(element.ToStringImpl(indent + 1));
            }

            sb.AppendLine($"{i}</{Name}>");

            return sb.ToString();
        }
    }

    public class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText) /*Kad vracamo referencu na nas builder na taj nacin dobijamo fluent builder*/
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);

            return this;
        }

        public override string ToString()
        {
            return root.ToStringImpl(0);
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }
    }

    public class FluentBuilder
    {
        public static void MainFunc(string[] args)
        {
            /*Primer builder principa je StringBuilder*/
            var hello = "hello";

            var sb = new StringBuilder();

            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");

            Console.WriteLine(sb);
            /**/

            var words = new[] { "hello", "world" };
            sb.Clear();

            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat($"<li>{word}</li>");
            }
            sb.Append("</ul>");
            Console.WriteLine(sb);

            // Ovde se vec da za primetiti da za kreiranje html elemenata vec imamo dosta manipulacija , razmotriti html builder
            // Pocinjemo od builder elementa


            var htmlBuilder = new HtmlBuilder("ul");
            htmlBuilder.AddChild("li", "hello").AddChild("li", "world");
            Console.WriteLine(htmlBuilder.ToString());

        }
    }
}
