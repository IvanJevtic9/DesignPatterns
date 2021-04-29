using ChainOfResponsibility.Br;
using ChainOfResponsibility.Exercise;
using System;

namespace ChainOfResponsibility
{
    public class Program
    {
        static void Main(string[] args)
        {
            MethodChain.MainFunc(args);
            Console.WriteLine();
            BrokerChain.MainFunc(args);

            ChainExercise.MainFunc(args);
        }
    }
}
