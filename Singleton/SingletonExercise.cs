using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton
{
    public class SingletonTester
    {
        public static bool IsSingleton(Func<object> func)
        {
            var object1 = func();
            var object2 = func();

            return ReferenceEquals(object1, object2);
        }
    }

    public class SingletonExercise
    {
        public static void MainFunc(string[] args)
        {
            var obj = new object();

            Console.WriteLine(SingletonTester.IsSingleton(() => obj));
            Console.WriteLine(SingletonTester.IsSingleton(() => new object()));
        }
    }
}
