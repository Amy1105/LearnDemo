using System.Collections.Concurrent;

namespace SearchString.ConcurrentFiles
{
    /// <summary>
    /// BlockingCollection<T>为实现 IProducerConsumerCollection<T>的线程安全集合提供阻塞和边界功能。
    /// 
    /// ConcurrentBag<T>表示对象的线程安全无序集合。
    /// 
    /// ConcurrentDictionary<TKey,TValue>表示可同时由多个线程访问的键/值对的线程安全集合。
    /// 
    /// ConcurrentQueue<T>	表示线程安全的先出 （FIFO） 集合。
    /// ConcurrentStack<T>	表示线程安全最后一次传入 （LIFO） 集合。
    /// 
    /// OrderablePartitioner<TSource>表示将可排序数据源拆分为多个分区的特定方式。
    /// 
    /// Partitioner	为数组、列表和可枚举提供常见的分区策略。
    /// Partitioner<TSource>表示将数据源拆分为多个分区的特定方式。
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
        /// BlockingCollection<T>   为实现 IProducerConsumerCollection<T>的线程安全集合提供阻塞和边界功能。
        /// </summary>
        public void BlockingCollectionMethod()
        {

        }    
    }
}