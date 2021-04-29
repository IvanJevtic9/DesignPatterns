using Proxy.Exercise;
using System;

namespace Proxy
{
    public class Program
    {
        static void Main(string[] args)
        {
            ProtectionProxy.MainFunc(args);
            PropertyProxy.MainFunc(args);
            ValueProxy.MainFunc(args);

            DynamicProxy.MainFunc(args);

            ProxyExercise.MainFunc(args);

            BitFraggeringAlgo.MainFunc(args);
        }
    }
}
