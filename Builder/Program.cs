using Builder.Faceted;
using FunctionalBuilderNem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Builder
{
    public class Program
    {
        static void Main(string[] args)
        {
            FluentBuilder.MainFunc(args);
            FluentBuilderInheritence.MainFunc(args);

            FunctionalBuilder.MainFunc(args);
            FacetedBuilder.MainFunc(args);

            CodeBuilder.MainFunc(args);
        }
    }
}
