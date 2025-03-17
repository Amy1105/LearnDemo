using System.Collections.Concurrent;

namespace SearchString.ConcurrentFiles
{
    /// <summary>
    /// 
    /// IProducerConsumerCollection<T>
    /// ConcurrentBag<T>表示对象的线程安全无序集合。
    /// ConcurrentQueue<T>	表示线程安全的先出 （FIFO） 集合。
    /// ConcurrentStack<T>	表示线程安全最后一次传入 （LIFO） 集合。
    /// 
    /// BlockingCollection<T>为实现 IProducerConsumerCollection<T>的线程安全集合提供阻塞和边界功能。
    /// ConcurrentDictionary<TKey,TValue>表示可同时由多个线程访问的键/值对的线程安全集合。
    /// 
    /// OrderablePartitioner<TSource>表示将可排序数据源拆分为多个分区的特定方式。 
    /// Partitioner	为数组、列表和可枚举提供常见的分区策略。
    /// Partitioner<TSource>表示将数据源拆分为多个分区的特定方式。
    /// 
    /// </summary>
    public class Class1
    {

        /// <summary>
        /// 包可用于在排序无关紧要时存储对象，与集不同，包支持重复项,接受null值。 
        /// ConcurrentBag<T> 是一种线程安全包实现，针对同一线程将同时生成和使用存储在包中的数据的方案进行优化
        /// 
        /// ConcurrentBag<T> 的所有公共和受保护成员都是线程安全的，可以从多个线程并发使用。 
        /// 但是，ConcurrentBag<T> 实现的其中一个接口（包括扩展方法）访问的成员不能保证线程安全，并且可能需要由调用方同步。
        /// </summary>
        public void ConcurrentBagMethod()
        {
            ConcurrentBagDemo.Method();
        }



        /// <summary>
        /// 多线程实现生产-消费模式
        /// </summary>
        /// <param name="args"></param>
        public static void Method(string[] args)
        {
            // 创建一个线程安全的队列
            var queue = new ConcurrentQueue<int>();

            // 创建一个BlockingCollection来包装队列
            var blockingCollection = new BlockingCollection<int>(queue);

            // 启动生产者任务
            var producerTask = Task.Run(() => ProduceData(blockingCollection));

            // 启动消费者任务
            var consumerTask = Task.Run(() => ConsumeData(blockingCollection));

            // 等待任务完成
            Task.WaitAll(producerTask, consumerTask);

            Console.WriteLine("处理完成。");
        }

        static void ProduceData(BlockingCollection<int> blockingCollection)
        {
            for (int i = 0; i < 10; i++)
            {
                // 模拟生产数据
                Console.WriteLine($"生产数据: {i}");
                blockingCollection.Add(i);

                // 模拟生产延迟
                Thread.Sleep(100);
            }

            // 标记生产完成
            blockingCollection.CompleteAdding();
        }

        static void ConsumeData(BlockingCollection<int> blockingCollection)
        {
            foreach (var item in blockingCollection.GetConsumingEnumerable())
            {
                // 模拟消费数据
                Console.WriteLine($"消费数据: {item}");

                // 模拟消费延迟
                Thread.Sleep(200);
            }
        }


        /// <summary>
        /// IProducerConsumerCollection<T> 表示允许线程安全添加和删除数据的集合
        /// BlockingCollection<T> 用作 IProducerConsumerCollection<T> 实例的包装器，并允许从集合中删除尝试阻止，直到删除数据为止。
        /// BlockingCollection<T>   为实现 IProducerConsumerCollection<T>的线程安全集合提供阻塞和边界功能。
        /// </summary>
        public void BlockingCollectionMethod()
        {

        }

        /// <summary>
        /// 对于非常大的 ConcurrentDictionary<TKey,TValue> 对象，可以将 64 位系统上的最大数组大小增加到 2 GB（GB），
        /// 方法是在运行时环境中将 <gcAllowVeryLargeObjects> 配置元素设置为 true。
        /// </summary>
        public void ConcurrentDictionaryMethod()
        {
            ConcurrentDictionaryDemo.Method();
        }

        /// <summary>
        /// 表示线程安全的先出 （FIFO） 集合
        /// </summary>
        public void ConcurrentQueueMethod()
        {
            ConcurrentQueueDemo.Method();
        }


        public async void ConcurrentStackMethod()
        {
           await ConcurrentStackDemo.Method();
            
           await ConcurrentStackDemo.Method2();
        }

        /// <summary>
        /// 表示将可排序数据源拆分为多个分区的特定方式。
        /// </summary>
        public void OrderablePartitionerTSourceMethod()
        {

        }

        //Partitioner  提供针对数组、列表和可枚举项的常见分区策略

        //Partitioner<TSource>  表示将数据源拆分为多个分区的特定方式


    }


    public class ConcurrentBagDemo
    {
        // Demonstrates:
        //      ConcurrentBag<T>.Add()
        //      ConcurrentBag<T>.IsEmpty
        //      ConcurrentBag<T>.TryTake()
        //      ConcurrentBag<T>.TryPeek()
        public static void Method()
        {
            // Add to ConcurrentBag concurrently
            ConcurrentBag<int> cb = new ConcurrentBag<int>();
            List<Task> bagAddTasks = new List<Task>();
            for (int i = 0; i < 500; i++)
            {
                var numberToAdd = i;
                bagAddTasks.Add(Task.Run(() => cb.Add(numberToAdd)));
            }

            // Wait for all tasks to complete
            Task.WaitAll(bagAddTasks.ToArray());

            // Consume the items in the bag
            List<Task> bagConsumeTasks = new List<Task>();
            int itemsInBag = 0;
            while (!cb.IsEmpty)
            {
                bagConsumeTasks.Add(Task.Run(() =>
                {
                    int item;
                    if (cb.TryTake(out item))
                    {
                        Console.WriteLine(item);
                        Interlocked.Increment(ref itemsInBag);
                    }
                }));
            }
            Task.WaitAll(bagConsumeTasks.ToArray());

            Console.WriteLine($"There were {itemsInBag} items in the bag");

            // Checks the bag for an item
            // The bag should be empty and this should not print anything
            int unexpectedItem;
            if (cb.TryPeek(out unexpectedItem))
                Console.WriteLine("Found an item in the bag when it should be empty");
        }
    }


    /// <summary>
    /// 表示可同时由多个线程访问的键/值对的线程安全集合。
    /// </summary>
    class ConcurrentDictionaryDemo
    {
        // Demonstrates:
        //      ConcurrentDictionary<TKey, TValue> ctor(concurrencyLevel, initialCapacity)
        //      ConcurrentDictionary<TKey, TValue>[TKey]
        public static void Method()
        {
            // We know how many items we want to insert into the ConcurrentDictionary.
            // So set the initial capacity to some prime number above that, to ensure that
            // the ConcurrentDictionary does not need to be resized while initializing it.
            int NUMITEMS = 64;
            int initialCapacity = 101;

            // The higher the concurrencyLevel, the higher the theoretical number of operations
            // that could be performed concurrently on the ConcurrentDictionary.  However, global
            // operations like resizing the dictionary take longer as the concurrencyLevel rises.
            // For the purposes of this example, we'll compromise at numCores * 2.
            int numProcs = Environment.ProcessorCount;
            int concurrencyLevel = numProcs * 2;

            // Construct the dictionary with the desired concurrencyLevel and initialCapacity
            ConcurrentDictionary<int, int> cd = new ConcurrentDictionary<int, int>(concurrencyLevel, initialCapacity);

            // Initialize the dictionary
            for (int i = 0; i < NUMITEMS; i++) cd[i] = i * i;

            Console.WriteLine("The square of 23 is {0} (should be {1})", cd[23], 23 * 23);
        }
    }

    public class ConcurrentQueueDemo
    {
        // Demonstrates:
        // ConcurrentQueue<T>.Enqueue()
        // ConcurrentQueue<T>.TryPeek()
        // ConcurrentQueue<T>.TryDequeue()
        public static void Method()
        {
            // Construct a ConcurrentQueue.
            ConcurrentQueue<int> cq = new ConcurrentQueue<int>();

            // Populate the queue.
            for (int i = 0; i < 10000; i++)
            {
                cq.Enqueue(i);
            }

            // Peek at the first element.
            int result;
            if (!cq.TryPeek(out result))
            {
                Console.WriteLine("CQ: TryPeek failed when it should have succeeded");
            }
            else if (result != 0)
            {
                Console.WriteLine("CQ: Expected TryPeek result of 0, got {0}", result);
            }

            int outerSum = 0;
            // An action to consume the ConcurrentQueue.
            Action action = () =>
            {
                int localSum = 0;
                int localValue;
                while (cq.TryDequeue(out localValue)) localSum += localValue;
                Interlocked.Add(ref outerSum, localSum);
            };

            // Start 4 concurrent consuming actions.
            Parallel.Invoke(action, action, action, action);

            Console.WriteLine("outerSum = {0}, should be 49995000", outerSum);
        }
    }


    public class ConcurrentStackDemo
    {
        // Demonstrates:
        //      ConcurrentStack<T>.Push();
        //      ConcurrentStack<T>.TryPeek();
        //      ConcurrentStack<T>.TryPop();
        //      ConcurrentStack<T>.Clear();
        //      ConcurrentStack<T>.IsEmpty;
      public  static async Task Method()
        {
            int items = 10000;

            ConcurrentStack<int> stack = new ConcurrentStack<int>();

            // Create an action to push items onto the stack
            Action pusher = () =>
            {
                for (int i = 0; i < items; i++)
                {
                    stack.Push(i);
                }
            };

            // Run the action once
            pusher();

            if (stack.TryPeek(out int result))
            {
                Console.WriteLine($"TryPeek() saw {result} on top of the stack.");
            }
            else
            {
                Console.WriteLine("Could not peek most recently added number.");
            }

            // Empty the stack
            stack.Clear();

            if (stack.IsEmpty)
            {
                Console.WriteLine("Cleared the stack.");
            }

            // Create an action to push and pop items
            Action pushAndPop = () =>
            {
                Console.WriteLine($"Task started on {Task.CurrentId}");

                int item;
                for (int i = 0; i < items; i++)
                    stack.Push(i);
                for (int i = 0; i < items; i++)
                    stack.TryPop(out item);

                Console.WriteLine($"Task ended on {Task.CurrentId}");
            };

            // Spin up five concurrent tasks of the action
            var tasks = new Task[5];
            for (int i = 0; i < tasks.Length; i++)
                tasks[i] = Task.Factory.StartNew(pushAndPop);

            // Wait for all the tasks to finish up
            await Task.WhenAll(tasks);

            if (!stack.IsEmpty)
            {
                Console.WriteLine("Did not take all the items off the stack");
            }
        }

        // Demonstrates:
        //      ConcurrentStack<T>.PushRange();
        //      ConcurrentStack<T>.TryPopRange();
        public static async Task Method2()
        {
            int numParallelTasks = 4;
            int numItems = 1000;
            var stack = new ConcurrentStack<int>();

            // Push a range of values onto the stack concurrently
            await Task.WhenAll(Enumerable.Range(0, numParallelTasks).Select(i => Task.Factory.StartNew((state) =>
            {
                // state = i * numItems
                int index = (int)state;
                int[] array = new int[numItems];
                for (int j = 0; j < numItems; j++)
                {
                    array[j] = index + j;
                }

                Console.WriteLine($"Pushing an array of ints from {array[0]} to {array[numItems - 1]}");
                stack.PushRange(array);
            }, i * numItems, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default)).ToArray());

            int numTotalElements = 4 * numItems;
            int[] resultBuffer = new int[numTotalElements];
            await Task.WhenAll(Enumerable.Range(0, numParallelTasks).Select(i => Task.Factory.StartNew(obj =>
            {
                int index = (int)obj;
                int result = stack.TryPopRange(resultBuffer, index, numItems);

                Console.WriteLine($"TryPopRange expected {numItems}, got {result}.");
            }, i * numItems, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default)).ToArray());

            for (int i = 0; i < numParallelTasks; i++)
            {
                // Create a sequence we expect to see from the stack taking the last number of the range we inserted
                var expected = Enumerable.Range(resultBuffer[i * numItems + numItems - 1], numItems);

                // Take the range we inserted, reverse it, and compare to the expected sequence
                var areEqual = expected.SequenceEqual(resultBuffer.Skip(i * numItems).Take(numItems).Reverse());
                if (areEqual)
                {
                    Console.WriteLine($"Expected a range of {expected.First()} to {expected.Last()}. Got {resultBuffer[i * numItems + numItems - 1]} to {resultBuffer[i * numItems]}");
                }
                else
                {
                    Console.WriteLine($"Unexpected consecutive ranges.");
                }
            }
        }
    }

}