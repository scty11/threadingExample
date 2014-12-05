using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class PrimativeAtomicAction
    {
        static int sum = 0;

        public static void Example()
        {
            Thread[] threads = new Thread[10];

            for (int n = 0; n < threads.Length; n++)
            {
                threads[n] = new Thread(AddOne);
                threads[n].Start();
            }

            for (int n = 0; n < threads.Length; n++)
            {
                threads[n].Join();
            }

            Console.WriteLine(
                "[{0}] sum = {1}",
                Thread.CurrentThread.ManagedThreadId,
                sum
            );
        }

#if (false)
    // Buggy version.
    static void AddOne()
    {
        Console.WriteLine("[{0}] AddOne called",
                          Thread.CurrentThread.ManagedThreadId);
        int temp = sum;
        temp++;
        Thread.Sleep(1);
        sum = temp;
    }
#else
        // Thread-safe version.
        static void AddOne()
        {
            Console.WriteLine("[{0}] AddOne called",
                              Thread.CurrentThread.ManagedThreadId);
            //this is a single atomic action.
            Interlocked.Increment(ref sum);
        }
#endif
    }
}
