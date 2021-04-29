using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Singleton
{
    public sealed class PerThreadSingleton
    {
        // Po threadu ce biti singleton, Lazy vraca uvek isto.
        private static ThreadLocal<PerThreadSingleton> threadInstance = new ThreadLocal<PerThreadSingleton>(() => new PerThreadSingleton());
        private PerThreadSingleton()
        {
            ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        public int ThreadId { get; }
        public static PerThreadSingleton Instance => threadInstance.Value;
    }

    public class ThreadSingleton
    {
        public static void MainFunc(string[] args)
        {
            var t1 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("t1 " + PerThreadSingleton.Instance.ThreadId);
            });

            var t2 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("t2 " + PerThreadSingleton.Instance.ThreadId);
                Console.WriteLine("t2 " + PerThreadSingleton.Instance.ThreadId);
            });

            Task.WaitAll(t1, t2);
        }
    }
}
