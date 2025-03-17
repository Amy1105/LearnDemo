using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchString.ConcurrentFiles
{
  
    class BlockingCollectionDemo
    {
      public  static async Task Method()
        {
            await AddTakeDemo.BC_AddTakeCompleteAdding();
            TryTakeDemo.BC_TryTake();
            FromToAnyDemo.BC_FromToAny();
            await ConsumingEnumerableDemo.BC_GetConsumingEnumerable();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
    class AddTakeDemo
    {
        // Demonstrates:
        //      BlockingCollection<T>.Add()
        //      BlockingCollection<T>.Take()
        //      BlockingCollection<T>.CompleteAdding()
        public static async Task BC_AddTakeCompleteAdding()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                // 启动任务以填充BlockingCollection
                Task t1 = Task.Run(() =>
                {
                    bc.Add(1);
                    bc.Add(2);
                    bc.Add(3);
                    bc.CompleteAdding();
                });

                // Spin up a Task to consume the BlockingCollection
                Task t2 = Task.Run(() =>
                {
                    try
                    {
                        // Consume the BlockingCollection
                        while (true) Console.WriteLine(bc.Take());
                    }
                    catch (InvalidOperationException)
                    {
                        // An InvalidOperationException means that Take() was called on a completed collection
                        Console.WriteLine("That's All!");
                    }
                });

                await Task.WhenAll(t1, t2);
            }
        }
    }

    class TryTakeDemo
    {
        // Demonstrates:
        //      BlockingCollection<T>.Add()
        //      BlockingCollection<T>.CompleteAdding()
        //      BlockingCollection<T>.TryTake()
        //      BlockingCollection<T>.IsCompleted
        public static void BC_TryTake()
        {
            // 构建并填充我们的BlockingCollection
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                int NUMITEMS = 10000;
                for (int i = 0; i < NUMITEMS; i++) bc.Add(i);
                bc.CompleteAdding();
                int outerSum = 0;

                // 代表使用BlockingCollection并将所有项目相加
                Action action = () =>
                {
                    int localItem;
                    int localSum = 0;

                    while (bc.TryTake(out localItem)) localSum += localItem;
                    Interlocked.Add(ref outerSum, localSum);
                };

                //启动三个并行操作以使用BlockingCollection
                Parallel.Invoke(action, action, action);

                Console.WriteLine("Sum[0..{0}) = {1}, should be {2}", NUMITEMS, outerSum, ((NUMITEMS * (NUMITEMS - 1)) / 2));
                Console.WriteLine("bc.IsCompleted = {0} (should be true)", bc.IsCompleted);
            }
        }
    }

    class FromToAnyDemo
    {
        // Demonstrates:
        //      Bounded BlockingCollection<T>
        //      BlockingCollection<T>.TryAddToAny()
        //      BlockingCollection<T>.TryTakeFromAny()
        public static void BC_FromToAny()
        {
            BlockingCollection<int>[] bcs = new BlockingCollection<int>[2];
            bcs[0] = new BlockingCollection<int>(5); // 集合限制为5个项目
            bcs[1] = new BlockingCollection<int>(5); // 集合限制为5个项目

            // 应该能够添加10个项目，但不阻止
            int numFailures = 0;
            for (int i = 0; i < 10; i++)
            {
                if (BlockingCollection<int>.TryAddToAny(bcs, i) == -1) numFailures++;
            }
            Console.WriteLine("TryAddToAny: {0} failures (should be 0)", numFailures);

            //应该能够检索10个项目
            int numItems = 0;
            int item;
            while (BlockingCollection<int>.TryTakeFromAny(bcs, out item) != -1) numItems++;
            Console.WriteLine("TryTakeFromAny: retrieved {0} items (should be 10)", numItems);
        }
    }

    class ConsumingEnumerableDemo
    {
        // Demonstrates:
        //      BlockingCollection<T>.Add()
        //      BlockingCollection<T>.CompleteAdding()
        //      BlockingCollection<T>.GetConsumingEnumerable()
        public static async Task BC_GetConsumingEnumerable()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                // 启动生产者任务
                var producerTask = Task.Run(async () =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        bc.Add(i);
                        Console.WriteLine($"Producing: {i}");
                        await Task.Delay(100); // sleep 100 ms between adds
                    }
                    //需要这样做才能防止下面的foreach挂起
                    bc.CompleteAdding();
                });

                //现在使用foreach使用阻塞集合。
                // Use bc.GetConsumingEnumerable() instead of just bc because the former will block waiting for completion and the latter will simply take a snapshot of the current state of the underlying collection.
                //使用bc。GetConsumingEnumerable（）而不仅仅是bc，因为前者会阻塞等待完成，而后者只会对底层集合的当前状态进行快照。

                foreach (var item in bc.GetConsumingEnumerable())
                {
                    Console.WriteLine($"Consuming: {item}");
                }
                await producerTask; //允许任务完成清理
            }
        }
    }
}
