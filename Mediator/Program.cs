using RxDemos.ImplementingObservable.Broker;
using System;

namespace Mediator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ChatExample.MainFunc(args);
            EventBrokerExample.MainB(args);
        }
    }
}
