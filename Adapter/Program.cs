using System;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            VectorDemo.MainFunc(args);
            GenericAdapterDemo.MainFunc(args);
            DependencyIncetionAdapter.MainFunc(args);
        }
    }
}
