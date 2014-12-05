using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class HelloThread
    {
        public static void Example()
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("[{0}] Processor/core count = {1}",
                              Thread.CurrentThread.ManagedThreadId,
                              Environment.ProcessorCount);
            String aString = "This is the string as an example passed in";

            Thread t = new Thread(SayHello);
            Thread u = new Thread(AnArgumentEx);

            t.Name = "Hello Thread";
            t.Priority = ThreadPriority.BelowNormal;
            t.Start();
            u.Start(aString);
            //waits until the thread is finnished to continue.
            t.Join();

            Console.WriteLine("[{0}] Main done", Thread.CurrentThread.ManagedThreadId);
        }

        static void SayHello()
        {

            Console.WriteLine("[{0}] Hello, world!", Thread.CurrentThread.ManagedThreadId);
        }

        static void AnArgumentEx(object anArg)
        {
            Console.WriteLine(anArg);
        }

        public static void SecondExample()
        {
            Console.WriteLine("[{0}] Main called", Thread.CurrentThread.ManagedThreadId);

            for (int n = 0; n < 10; n++)
            {
                ThreadPool.QueueUserWorkItem(SayHelloPool, n);
            }

            Thread.Sleep(rng.Next(1000, 3000));
            Console.WriteLine("[{0}] Main done", Thread.CurrentThread.ManagedThreadId);
        }

        static Random rng = new Random();

        static void SayHelloPool(object arg)
        {
            Thread.Sleep(rng.Next(250, 500));

            int n = (int)arg;

            Console.WriteLine("[{0}] Hello, world {1}! ({2})",
                              Thread.CurrentThread.ManagedThreadId,
                              n,
                              Thread.CurrentThread.IsBackground);
        }
    }
}
