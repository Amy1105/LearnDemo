using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace net9Demo
{
    /// <summary>
    /// 以下是 C# 中锁的实战练习，涵盖基础用法、常见陷阱和高级场景。建议先独立实现，再参考参考答案
    /// 练习建议
    /// 先独立实现每个练习，再对比参考答案。
    /// 使用单元测试验证代码的线程安全性（如Parallel.For模拟并发）。
    /// 对练习 5 和 6，故意制造死锁或异常，观察程序行为。
    /// 思考每个练习中锁的选择是否最优，是否有其他实现方式。
    /// 
    /// 
    /// </summary>
    internal partial class Examples
    {

            /// <summary>
            /// 练习 1：基础锁实现
            /// 需求：实现一个线程安全的计数器，确保多线程并发调用Increment方法时计数正确。
            /// 提示：使用lock语句或Monitor类
            /// </summary>
public class ThreadSafeCounter
        {
            private int _count = 0;
            private readonly object _lock = new object();

            public void Increment()
            {
                // TODO: 实现线程安全的自增
            }

            public int GetCount()
            {
                // TODO: 实现线程安全的读取
                return 0;
            }
        }

      
/// <summary>
/// 练习 2：双重检查锁定（DCL）
///  需求：实现单例模式的双重检查锁定，确保线程安全且高效。
/// 提示：使用volatile关键字和两次空检查
/// 
/// </summary>
public sealed class Singleton
        {
            private static volatile Singleton _instance;
            private static readonly object _lock = new object();

            private Singleton() { }

            public static Singleton Instance
            {
                get
                {
                    // TODO: 实现双重检查锁定
                }
            }
        }



    /// <summary>
    /// 练习 3：生产者 - 消费者队列
    /// 需求：使用ConcurrentQueue和ManualResetEventSlim实现一个线程安全的生产者 - 消费者队列，支持：
    /// 多个生产者并发添加任务
    /// 多个消费者并发处理任务
    /// 队列空时消费者阻塞等待
    /// 支持优雅关闭
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TaskQueue<T> : IDisposable
        {
            private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();
            private readonly ManualResetEventSlim _signal = new ManualResetEventSlim(false);
            private bool _isStopping = false;

            public void Enqueue(T task)
            {
                // TODO: 添加任务并通知消费者
            }

            public bool TryDequeue(out T task, int timeout = Timeout.Infinite)
            {
                // TODO: 尝试获取任务，队列为空时阻塞等待
                return false;
            }

            public void Stop()
            {
                // TODO: 优雅关闭队列
            }

            public void Dispose()
            {
                // TODO: 释放资源
            }
        }


        /// <summary>
        /// 练习 4：读写锁优化
        /// 需求：将以下使用lock的代码优化为使用ReaderWriterLockSlim，提高读性能：
        /// </summary>
        public class DataCache
        {
            private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();
            private readonly object _lock = new object();

            public object Get(string key)
            {
                lock (_lock)
                {
                    _cache.TryGetValue(key, out var value);
                    return value;
                }
            }

            public void Add(string key, object value)
            {
                lock (_lock)
                {
                    _cache[key] = value;
                }
            }
        }

        /// <summary>
        /// 练习 5：死锁调试
        /// 需求：分析以下代码中的死锁原因，并修复：
        /// </summary>
        public class DeadlockExample
        {
            private readonly object _lock1 = new object();
            private readonly object _lock2 = new object();

            public void Method1()
            {
                lock (_lock1)
                {
                    Thread.Sleep(100);
                    lock (_lock2)
                    {
                        Console.WriteLine("Method1 completed");
                    }
                }
            }

            public void Method2()
            {
                lock (_lock2)
                {
                    Thread.Sleep(100);
                    lock (_lock1)
                    {
                        Console.WriteLine("Method2 completed");
                    }
                }
            }
        }

/// <summary>
/// 练习 6：自定义不可重入锁
/// 需求：实现一个自定义的不可重入锁，当检测到嵌套锁时抛出异常。
/// 提示：跟踪当前持有锁的线程。
/// </summary>
    public class NonReentrantLock : IDisposable
        {
            private readonly object _syncRoot = new object();
            private Thread _ownerThread = null;
            private bool _isLocked = false;

            public void Enter()
            {
                // TODO: 获取锁，检测嵌套
            }

            public void Exit()
            {
                // TODO: 释放锁
            }

            public void Dispose()
            {
                // TODO: 释放资源
            }
        }


  /// <summary>
  /// 练习 7：异步锁实现
  /// 需求：实现一个支持异步等待的锁，用于保护异步代码中的临界区。
  /// 提示：使用SemaphoreSlim。
  /// </summary>
    public class AsyncLock : IDisposable
        {
            private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
            private readonly Task<Releaser> _releaser;

            public AsyncLock()
            {
                _releaser = Task.FromResult(new Releaser(this));
            }

            public Task<Releaser> LockAsync()
            {
                // TODO: 异步获取锁
            }

            public void Release()
            {
                // TODO: 释放锁
            }

            public void Dispose()
            {
                _semaphore.Dispose();
            }

            public readonly struct Releaser : IDisposable
            {
                private readonly AsyncLock _toRelease;

                internal Releaser(AsyncLock toRelease) => _toRelease = toRelease;

                public void Dispose() => _toRelease?.Release();
            }
        }



        /// <summary>
        /// 练习 1 参考答案
        /// </summary>
        public void Increment()
        {
            lock (_lock)
            {
                _count++;
            }
        }

        public int GetCount()
        {
            lock (_lock)
            {
                return _count;
            }
        }
        /// <summary>
        /// 
        练习 2 参考答案
/// </summary>
public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Singleton();
                        }
                    }
                }
                return _instance;
            }
        }
        /// <summary>
        /// 练习 5 参考答案（修复死锁）
        /// 确保两个方法以相同顺序获取锁
        /// </summary>
        public void Method1()
        {
            lock (_lock1)
            {
                Thread.Sleep(100);
                lock (_lock2)
                {
                    Console.WriteLine("Method1 completed");
                }
            }
        }

        public void Method2()
        {
            lock (_lock1)  // 与Method1顺序一致
            {
                Thread.Sleep(100);
                lock (_lock2)
                {
                    Console.WriteLine("Method2 completed");
                }
            }
        }

    }
}
