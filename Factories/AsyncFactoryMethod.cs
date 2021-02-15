using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Factories
{
    public class AsyncFactoryMethod
    {
        private double x;
        public AsyncFactoryMethod(double x)
        {
            this.x = x;
        }

        private async Task<AsyncFactoryMethod> InitAsync()
        {
            await Task.Delay(1000);
            Console.WriteLine("Task has been completed!");
            return this;
        }

        public static Task<AsyncFactoryMethod> CreateAsync(double x)
        {
            Console.WriteLine("Initializing");
            var p = new AsyncFactoryMethod(x);

            return p.InitAsync();
        }

        public static async Task MainFuncAsync(string[] args)
        {
            var asy = await AsyncFactoryMethod.CreateAsync(100);
            Console.WriteLine(asy.x);
        }
    }
}
