﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class DataPartionExample
    {
        public static void Example()
        {
            // Determine how many cores/processors there are on this machine.
            //
            int coreCount = Environment.ProcessorCount;

            Console.WriteLine("Process/core count = {0}", coreCount);

            // Get some data to work with.
            //
            double[] data = GetData();

            Stopwatch sw = Stopwatch.StartNew();

            // Setup same-sized arrays of references to ArraySlice
            // Thread objects; one per core/processor.
            // 
            ArrayProcessor[] slices = new ArrayProcessor[coreCount];
            Thread[] threads = new Thread[coreCount];

            // Divide the work (roughly) evenly among the
            // number of threads we're about to start.  The last
            // thread will pickup any leftovers if the data size
            // is not evenly divisible by the core count.
            //
            int indexesPerThread = data.Length / coreCount;
            int leftOverIndexes = data.Length % coreCount;

            for (int n = 0; n < coreCount; n++)
            {
                int firstIndex = (n * indexesPerThread);
                int lastIndex = firstIndex + indexesPerThread - 1;

                if (n == (coreCount - 1))
                {
                    lastIndex += leftOverIndexes;
                }

                // Setup the array slice that describes this
                // portion of the array.
                //
                ArrayProcessor slice = new ArrayProcessor(data, firstIndex, lastIndex);
                slices[n] = slice;

                // Start the thread that will process this slice.
                //
                threads[n] = new Thread(slice.ComputeSum);
                threads[n].Start();
            }

            // Once all the threads have been started, wait for
            // them to complete their assigned computations, then
            // add their sum to the running total.
            //
            double sum = 0;

            for (int n = 0; n < coreCount; n++)
            {
                threads[n].Join();
                sum += slices[n].Sum;
            }

            sw.Stop();

            // Display the results.
            //
            Console.WriteLine(
                "{0} threads computed {1:n0} in {2:n0} ms",
                coreCount, sum, sw.ElapsedMilliseconds
            );
        }

        static double[] GetData()
        {
            double[] data = new double[5000];

            for (int n = 0; n < data.Length; n++)
            {
                data[n] = n;
            }

            return (data);
        }
    }

    class ArrayProcessor
    {
        double[] _data;
        int _firstIndex;
        int _lastIndex;
        double _sum;

        public ArrayProcessor(double[] data, int firstIndex, int lastIndex)
        {
            _data = data;
            _firstIndex = firstIndex;
            _lastIndex = lastIndex;
        }

        public void ComputeSum()
        {
            _sum = 0;

            for (int n = _firstIndex; n <= _lastIndex; n++)
            {
                _sum += _data[n];
                Thread.Sleep(1);
            }
        }

        public double Sum
        {
            get { return (_sum); }
        }
    }

}
