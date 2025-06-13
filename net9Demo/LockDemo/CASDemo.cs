using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo.LockDemo
{
    /// <summary>
    /// CAS（Compare-And-Swap，比较并交换）是一种无锁（Lock-Free）原子操作，用于实现线程安全的并发编程。
    /// 它是现代多核 CPU 提供的硬件级原子指令，被广泛应用于各种同步机制（如锁、无锁数据结构等）
    /// </summary>

    public class CASDemo
    {
        /// <summary>
        /// 使用CAS实现自旋锁Spinlock
        /// </summary>
        public class CASWithSpinLock
        {
            private int _lock = 0; // 0=未锁定, 1=锁定

            public void Enter()
            {
                while (Interlocked.CompareExchange(ref _lock, 1, 0) != 0)
                {
                    // 自旋等待（Spin Wait）
                }
            }

            public void Exit()
            {
                _lock = 0; // 或 Interlocked.Exchange(ref _lock, 0)
            }
        }
        /// <summary>
        /// 使用CAS实现无锁计数器
        /// </summary>
        public class NoLockCounter
        {
            private int _counter = 0;

           public void Increment()
            {
                int oldValue, newValue;
                do
                {
                    oldValue = _counter;
                    newValue = oldValue + 1;
                } while (Interlocked.CompareExchange(ref _counter, newValue, oldValue) != oldValue);
            }
        }

        /// <summary>
        /// 使用CAS实现线程安全的单例模式，伪代码
        /// </summary>
        public class CASWithSinglePattern
        {
            private static object _instance;
            private static readonly object _lock = new object();

            public static object Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        var temp = new object();
                        Interlocked.CompareExchange(ref _instance, temp, null);
                    }
                    return _instance;
                }
            }
        }
    }
}
