using System;
using System.Threading.Tasks;

namespace Factories
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Point.MainFunc(args);
            await AsyncFactoryMethod.MainFuncAsync(args);

            //AbstractFactory.MainFunc(args);

            FactoryExercise.MainFunc(args);
        }
    }
}
