using Decorator.Static;
using System;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomStringBuilder.MainFunc(args);
            AdapterDecorator.MainFunc(args);
            MultipleInheritanceWithDefaultMemberExample.MainFunc(args);
            DynamicDecorator.MainFunc(args);
            StaticDecoratorComposition.MainFunc(args);
            Decorators.MainFunc(args);
        }
    }
}
