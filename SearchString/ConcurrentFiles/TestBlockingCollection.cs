using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchString.ConcurrentFiles
{

    // 实现具有边界和阻塞功能的优先级队列。
    public class SimplePriorityQueue<TPriority, TValue> : IProducerConsumerCollection<KeyValuePair<int, TValue>>
    {
        // 数组中的每个内部队列代表一个优先级。
        // 给定数组中的所有元素具有相同的优先级。
        private ConcurrentQueue<KeyValuePair<int, TValue>>[] _queues = null;

        // 我们内部存储的队列数量。
        private int priorityCount = 0;
        private int m_count = 0;

        public SimplePriorityQueue(int priCount)
        {
            this.priorityCount = priCount;
            _queues = new ConcurrentQueue<KeyValuePair<int, TValue>>[priorityCount];
            for (int i = 0; i < priorityCount; i++)
                _queues[i] = new ConcurrentQueue<KeyValuePair<int, TValue>>();
        }

        // IProducerConsumerCollection members
        public bool TryAdd(KeyValuePair<int, TValue> item)
        {
            _queues[item.Key].Enqueue(item);
            Interlocked.Increment(ref m_count);
            return true;
        }

        public bool TryTake(out KeyValuePair<int, TValue> item)
        {
            bool success = false;

            // 按优先级顺序循环队列，寻找要出列的项目。
            for (int i = 0; i < priorityCount; i++)
            {
                //锁定内部数据，以便Dequeue操作和m_count的更新是原子性的。
                lock (_queues)
                {
                    success = _queues[i].TryDequeue(out item);
                    if (success)
                    {
                        Interlocked.Decrement(ref m_count);
                        return true;
                    }
                }
            }

            // 如果我们到了这里，我们什么也没找到。
            //将out参数指定为默认值并返回false。
            item = new KeyValuePair<int, TValue>(0, default(TValue));
            return false;
        }

        public int Count
        {
            get { return m_count; }
        }

        // Required for ICollection
        void ICollection.CopyTo(Array array, int index)
        {
            CopyTo(array as KeyValuePair<int, TValue>[], index);
        }

        // CopyTo在生产者和消费者中是有问题的。
        // 目标数组可能比我们从ToArray获得的数组短或长，这是由于在分配目标数组后进行了添加或获取。
        // 因此，我们在这里所要做的就是用尽可能多的数据填充目的地，而不会跑偏终点。
        public void CopyTo(KeyValuePair<int, TValue>[] destination, int destStartingIndex)
        {
            if (destination == null) throw new ArgumentNullException();
            if (destStartingIndex < 0) throw new ArgumentOutOfRangeException();

            int remaining = destination.Length;
            KeyValuePair<int, TValue>[] temp = this.ToArray();
            for (int i = 0; i < destination.Length && i < temp.Length; i++)
                destination[i] = temp[i];
        }

        public KeyValuePair<int, TValue>[] ToArray()
        {
            KeyValuePair<int, TValue>[] result;

            lock (_queues)
            {
                result = new KeyValuePair<int, TValue>[this.Count];
                int index = 0;
                foreach (var q in _queues)
                {
                    if (q.Count > 0)
                    {
                        q.CopyTo(result, index);
                        index += q.Count;
                    }
                }
                return result;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<int, TValue>> GetEnumerator()
        {
            for (int i = 0; i < priorityCount; i++)
            {
                foreach (var item in _queues[i])
                    yield return item;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public object SyncRoot
        {
            get { throw new NotSupportedException(); }
        }
    }

    public class TestBlockingCollection
    {
       public static void Method()
        {

            int priorityCount = 7;
            SimplePriorityQueue<int, int> queue = new SimplePriorityQueue<int, int>(priorityCount);
            var bc = new BlockingCollection<KeyValuePair<int, int>>(queue, 500);

            CancellationTokenSource cts = new CancellationTokenSource();

            Task.Run(() =>
            {
                if (Console.ReadKey(true).KeyChar == 'c')
                    cts.Cancel();
            });

            // 创建一个Task数组，以便我们可以等待它并捕获任何异常，包括用户取消。
            Task[] tasks = new Task[2];

            // 创建生产者线程。您可以更改代码，使等待时间比消费者线程慢一点，以演示阻塞功能。
            tasks[0] = Task.Run(() =>
            {
                // 我们随机化等待时间，并使用该值来确定项目的优先级（Key）。
                Random r = new Random();

                int itemsToAdd = 500;
                int count = 0;
                while (!cts.Token.IsCancellationRequested && itemsToAdd-- > 0)
                {
                    int waitTime = r.Next(2000);
                    int priority = waitTime % priorityCount;
                    var item = new KeyValuePair<int, int>(priority, count++);

                    bc.Add(item);
                    Console.WriteLine("added pri {0}, data={1}", item.Key, item.Value);
                }
                Console.WriteLine("Producer is done adding.");
                bc.CompleteAdding();
            },
             cts.Token);

            //让制作人有机会添加一些项目。
            Thread.SpinWait(1000000);

            // Create a consumer thread. The wait time is a bit slower than the producer thread to demonstrate the bounding capability at the high end. Change this value to see the consumer run faster to demonstrate the blocking functionality at the low end.
            //创建一个消费者线程。等待时间比生产者线程稍慢，以展示高端的边界功能。更改此值可以看到消费者运行得更快，以演示低端的阻塞功能。

            tasks[1] = Task.Run(() =>
            {
                while (!bc.IsCompleted && !cts.Token.IsCancellationRequested)
                {
                    Random r = new Random();
                    int waitTime = r.Next(2000);
                    Thread.SpinWait(waitTime * 70);

                    // KeyValuePair is a value type. Initialize to avoid compile error in if(success)
                    //KeyValuePair是一种值类型。初始化以避免if（成功）中的编译错误
                    KeyValuePair<int, int> item = new KeyValuePair<int, int>();
                    bool success = false;
                    success = bc.TryTake(out item);
                    if (success)
                    {
                        //对数据做一些有用的事情。
                        Console.WriteLine("removed Pri = {0} data = {1} collCount= {2}", item.Key, item.Value, bc.Count);
                    }
                    else
                    {
                        Console.WriteLine("No items to retrieve. count = {0}", bc.Count);
                    }
                }
                Console.WriteLine("Exited consumer loop");
            },
                cts.Token);

            try
            {
                Task.WaitAll(tasks, cts.Token);
            }
            catch (OperationCanceledException e)
            {
                if (e.CancellationToken == cts.Token)
                    Console.WriteLine("Operation was canceled by user. Press any key to exit");
            }
            catch (AggregateException ae)
            {
                foreach (var v in ae.InnerExceptions)
                    Console.WriteLine(v.Message);
            }
            finally
            {
                cts.Dispose();
            }

            Console.ReadKey(true);
        }
    }
}