using System;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            ClonableExample.MainFunc(args);
            DeepCopyInterface.MainFunc(args);
            CopySerializer.MainFunc(args);
        }
    }
}
