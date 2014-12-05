using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //HelloThread.Example();
            //HelloThread.SecondExample();
            //PrimativeAtomicAction.Example();
            DataPartionExample.Example();
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
