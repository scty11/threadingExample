using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleApplication1
{
    public class LockExample
    {
        static object _lock = new object();
        static Queue<int> _aQueue = new Queue<int>();
        public static void Example()
        {
           
            Thread _input = new Thread(Output);
            Thread _output = new Thread(Input);
            _input.Start();
            _output.Start();

        }

        static void Input()
        {
            for (int i = 0; i < 10; i++)
            {
                lock (_lock)
                {
                    _aQueue.Enqueue(i);
                    Monitor.PulseAll(_lock);
                }
                Console.WriteLine("[{0}] added {1}", Thread.CurrentThread.ManagedThreadId, i);
                Thread.Sleep(1000);
            }
        }

        static void Output()
        {
            while (true)
            {
                lock (_lock)
                {
                while (_aQueue.Count == 0)
                {
                    Console.WriteLine("Queue is empty");
                    Monitor.Wait(_lock);
                }
                
                    int i = _aQueue.Dequeue();
                    Console.WriteLine("[{0}] output the value {1}", Thread.CurrentThread.ManagedThreadId, i);
                }
            }
        }
    }
}
