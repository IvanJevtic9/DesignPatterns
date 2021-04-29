using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            SingletonImpl.MainFunc();
            ThreadSingleton.MainFunc(args);
            AmbientContextExample.MainFunc(args);
            SingletonExercise.MainFunc(args);
        }
    }
}
