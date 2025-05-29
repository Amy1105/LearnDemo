using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo.LockDemo
{
    /// <summary>
    /// 通过以上练习，你可以掌握：
    /// 基础同步：lock 保护共享资源。
    /// 线程通信：Monitor.Wait/Pulse 实现生产者-消费者模型。
    /// 死锁预防：固定加锁顺序。
    /// 性能优化：ReaderWriterLockSlim 区分读写锁。
    /// 健壮性：带超时的锁获取。
    /// 
    /// 进阶挑战：尝试用 SemaphoreSlim、Mutex 或 SpinLock 实现类似功能，并对比性能差异。
    /// </summary>
    internal partial class Examples
    {
        /// <summary>
        /// 练习 1：线程安全的计数器
        /// 目标：实现一个多线程安全的计数器，避免并发写入导致的数据错误。
        /// 要求：使用 lock 关键字保护共享变量。启动 10 个线程，每个线程对计数器递增 1000 次。最终结果应为 10000。
        /// </summary>
        public class ThreadSafeCounter
        {
            private int _count = 0;
            private readonly object _lock = new object();
            public void Increment()
            {
                lock (_lock)
                {
                    _count++;
                }
            }
            public int GetCount() => _count;
        }

        public async void TestThreadSafeCounter()
        {
            // 测试代码
            var counter = new ThreadSafeCounter();
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        counter.Increment();
                    }
                }));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine($"Final count: {counter.GetCount()}"); // 应输出 10000
        }

        /// <summary>
        /// 练习 2：生产者-消费者队列
        /// 目标：实现一个线程安全的队列，生产者插入数据，消费者取出数据。
        /// 要求：使用 Monitor.Wait 和 Monitor.Pulse 实现线程间通信。当队列为空时，消费者线程应等待。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class BlockingQueue<T>
        {
            private readonly Queue<T> _queue = new Queue<T>();
            private readonly object _lock = new object();
            public void Enqueue(T item)
            {
                lock (_lock)
                {
                    _queue.Enqueue(item);
                    Monitor.Pulse(_lock); // 通知等待的消费者
                }
            }

            public T Dequeue()
            {
                lock (_lock)
                {
                    while (_queue.Count == 0)
                    {
                        Monitor.Wait(_lock); // 释放锁并等待
                    }
                    return _queue.Dequeue();
                }
            }
        }
        public async void TestBlockingQueue()
        {
            // 测试代码
            var queue = new BlockingQueue<int>();
            var producer = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    queue.Enqueue(i);
                    Thread.Sleep(100); // 模拟生产耗时
                }
            });
            var consumer = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    int item = queue.Dequeue();
                    Console.WriteLine($"Consumed: {item}");
                }
            });
            await Task.WhenAll(producer, consumer);
        }

        /// <summary>
        /// 
        /// </summary>
        public class BankAccount
        {
            public int Id { get; }
            public decimal Balance { get; private set; }
            private readonly object _balanceLock = new object();

            public BankAccount(int id, decimal balance)
            {
                Id = id;
                Balance = balance;
            }

            public void Transfer(BankAccount target, decimal amount)
            {
                var lock1 = Id < target.Id ? _balanceLock : target._balanceLock;
                var lock2 = Id < target.Id ? target._balanceLock : _balanceLock;

                lock (lock1)
                {
                    lock (lock2)
                    {
                        if (Balance >= amount)
                        {
                            Balance -= amount;
                            target.Balance += amount;
                            Console.WriteLine($"Transferred {amount} from {Id} to {target.Id}");
                        }
                    }
                }
            }
        }

        public async void TestBankAccount()
        {
            // 测试代码
            var accountA = new BankAccount(1, 1000);
            var accountB = new BankAccount(2, 1000);
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() => accountA.Transfer(accountB, 100)));
                tasks.Add(Task.Run(() => accountB.Transfer(accountA, 100)));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine($"Account A balance: {accountA.Balance}");
            Console.WriteLine($"Account B balance: {accountB.Balance}");
        }

        /// <summary>
        /// 练习 4：读写锁（ReaderWriterLockSlim）
        /// 目标：使用 ReaderWriterLockSlim 优化读多写少的场景。
        /// 要求：允许多个线程同时读取数据。写入时独占锁。
        /// </summary>
        public class Cache
        {
            private readonly Dictionary<string, string> _data = new Dictionary<string, string>();
            private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

            public string Read(string key)
            {
                _lock.EnterReadLock();
                try
                {
                    return _data.TryGetValue(key, out var value) ? value : null;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }

            public void Write(string key, string value)
            {
                _lock.EnterWriteLock();
                try
                {
                    _data[key] = value;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        public async void TestCache()
        {
            // 测试代码
            var cache = new Cache();
            cache.Write("name", "Alice");
            var readTasks = new List<Task>();
            for (int i = 0; i < 5; i++)
            {
                readTasks.Add(Task.Run(() => Console.WriteLine(cache.Read("name"))));
            }
            await Task.WhenAll(readTasks);
        }


        /// <summary>
        /// 练习 5：超时锁（Monitor.TryEnter）
        /// 目标：尝试获取锁，若超时则放弃。
        /// 要求：使用 Monitor.TryEnter 实现带超时的锁。超时后执行备用逻辑。
        /// </summary>
        public class Resource
        {
            private readonly object _lock = new object();

            public void AccessResource(int timeoutMs)
            {
                bool lockTaken = false;
                try
                {
                    Monitor.TryEnter(_lock, timeoutMs, ref lockTaken);
                    if (lockTaken)
                    {
                        Console.WriteLine("Lock acquired. Doing work...");
                        Thread.Sleep(2000); // 模拟耗时操作
                    }
                    else
                    {
                        Console.WriteLine("Failed to acquire lock. Executing fallback...");
                    }
                }
                finally
                {
                    if (lockTaken) Monitor.Exit(_lock);
                }
            }
        }

        public async void TestResource()
        {
            // 测试代码
            var resource = new Resource();
            var task1 = Task.Run(() => resource.AccessResource(1000));
            var task2 = Task.Run(() => resource.AccessResource(100));
            await Task.WhenAll(task1, task2);
        }
    }
}