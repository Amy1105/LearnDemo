using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchString.ConcurrentFiles
{
    
    /// <summary>
    /// 实现静态分区
    /// 一种静态范围划分器，用于需要线性增加每个后续元素处理时间的源。
    /// 范围大小是根据第一个分区获得最多元素和最后一个分区获得最少元素的增长率计算的。
    /// </summary>
    class MyPartitioner : Partitioner<int>
    {
        int[] source;
        double rateOfIncrease = 0;

        public MyPartitioner(int[] source, double rate)
        {
            this.source = source;
            rateOfIncrease = rate;
        }

        public override IEnumerable<int> GetDynamicPartitions()
        {
            throw new NotImplementedException();
        }

        // Not consumable from Parallel.ForEach.
        public override bool SupportsDynamicPartitions
        {
            get
            {
                return false;
            }
        }

        public override IList<IEnumerator<int>> GetPartitions(int partitionCount)
        {
            List<IEnumerator<int>> _list = new List<IEnumerator<int>>();
            int end = 0;
            int start = 0;
            int[] nums = CalculatePartitions(partitionCount, source.Length);

            for (int i = 0; i < nums.Length; i++)
            {
                start = nums[i];
                if (i < nums.Length - 1)
                    end = nums[i + 1];
                else
                    end = source.Length;

                _list.Add(GetItemsForPartition(start, end));

                // For demonstration.
                Console.WriteLine("start = {0} b (end) = {1}", start, end);
            }
            return (IList<IEnumerator<int>>)_list;
        }
        /*
         *
         *
         *                                                               B
          // Model increasing workloads as a right triangle           /  |
             divided into equal areas along vertical lines.         / |  |
             Each partition  is taller and skinnier               /   |  |
             than the last.                                     / |   |  |
                                                              /   |   |  |
                                                            /     |   |  |
                                                          /  |    |   |  |
                                                        /    |    |   |  |
                                                A     /______|____|___|__| C
         */
        private int[] CalculatePartitions(int partitionCount, int sourceLength)
        {
            // Corresponds to the opposite side of angle A, which corresponds
            // to an index into the source array.
            int[] partitionLimits = new int[partitionCount];
            partitionLimits[0] = 0;

            // Represent total work as rectangle of source length times "most expensive element"
            // Note: RateOfIncrease can be factored out of equation.
            double totalWork = sourceLength * (sourceLength * rateOfIncrease);
            // Divide by two to get the triangle whose slope goes from zero on the left to "most"
            // on the right. Then divide by number of partitions to get area of each partition.
            totalWork /= 2;
            double partitionArea = totalWork / partitionCount;

            // Draw the next partitionLimit on the vertical coordinate that gives
            // an area of partitionArea * currentPartition.
            for (int i = 1; i < partitionLimits.Length; i++)
            {
                double area = partitionArea * i;

                // Solve for base given the area and the slope of the hypotenuse.
                partitionLimits[i] = (int)Math.Floor(Math.Sqrt((2 * area) / rateOfIncrease));
            }
            return partitionLimits;
        }

        IEnumerator<int> GetItemsForPartition(int start, int end)
        {
            // For demonstration purposes. Each thread receives its own enumerator.
            Console.WriteLine("called on thread {0}", Thread.CurrentThread.ManagedThreadId);
            for (int i = start; i < end; i++)
                yield return source[i];
        }
    }

    class Consumer
    {
        public static void Main2()
        {
            var source = Enumerable.Range(0, 10000).ToArray();

            Stopwatch sw = Stopwatch.StartNew();
            MyPartitioner partitioner = new MyPartitioner(source, .5);

            var query = from n in partitioner.AsParallel()
                        select ProcessData(n);

            foreach (var v in query) { }
            Console.WriteLine("Processing time with custom partitioner {0}", sw.ElapsedMilliseconds);

            var source2 = Enumerable.Range(0, 10000).ToArray();

            sw = Stopwatch.StartNew();

            var query2 = from n in source2.AsParallel()
                         select ProcessData(n);

            foreach (var v in query2) { }
            Console.WriteLine("Processing time with default partitioner {0}", sw.ElapsedMilliseconds);
        }

        // Consistent processing time for measurement purposes.
        static int ProcessData(int i)
        {
            Thread.SpinWait(i * 1000);
            return i;
        }
    }
}
