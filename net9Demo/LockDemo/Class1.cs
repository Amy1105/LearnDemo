using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net9Demo.LockDemo
{
    /// <summary>
    /// 1.锁对象必须为引用类型
    /// 2.避免公开暴露锁对象
    /// 3.谨慎选择锁对象
    /// 4.调试辅助
    /// 
    /// 在 C# 里，对象锁其实就是以任意引用类型的对象为基础实现的锁机制。
    /// 这种机制主要依靠.NET 框架中的Monitor类来达成线程同步的目的。
    /// 任何引用类型的对象都能够充当锁的角色，这是因为每个对象在内存里都包含一个同步块索引，而这个索引正是用于实现锁功能的
    /// 
    /// 在高并发场景下，Concurrent* 集合通常表现更好
    /// Concurrent* 集合的优势在于高并发。如果并发度低，普通集合加锁可能更简单。
    /// </summary>
    public class Class1
    {
        /// <summary>
        /// 直接使用集合对象作为锁
        /// 直接复用 _idleConnections 作为锁对象，无需额外定义 private readonly object _lock = new object()。
        /// 所有操作共享同一锁，确保 Stack 的线程安全。  
        /// </summary>
        public class ConnectionPool
        {
            private readonly Stack<SocketsHttpConnectionContext> _idleConnections = new Stack<SocketsHttpConnectionContext>();

            // 获取连接（线程安全）
            public SocketsHttpConnectionContext RentConnection()
            {
                lock (_idleConnections) // 直接使用 Stack 对象作为锁
                {
                    if (_idleConnections.TryPop(out var connection))
                    {
                        return connection;
                    }
                    return CreateNewConnection();
                }
            }

            // 归还连接（线程安全）
            public void ReturnConnection(SocketsHttpConnectionContext connection)
            {
                lock (_idleConnections) // 同一个锁对象
                {
                    _idleConnections.Push(connection);
                }
            }
        }

        public class CacheDemo
        {
            private readonly Dictionary<string, object> _cache = new Dictionary<string, object>();

            public object GetOrAdd(string key, Func<object> valueFactory)
            {
                lock (_cache) // 直接使用 Dictionary 作为锁
                {
                    if (!_cache.TryGetValue(key, out var value))
                    {
                        value = valueFactory();
                        _cache.Add(key, value);
                    }
                    return value;
                }
            }
        }

        /// <summary>
        /// 生产者-消费者 加锁简单实现
        /// </summary>
        public class MessageQueue
        {
            private readonly Queue<string> _messages = new Queue<string>();
            public void Enqueue(string message)
            {
                lock (_messages) // 使用 Queue 作为锁
                {
                    _messages.Enqueue(message);
                    Monitor.Pulse(_messages); // 通知等待线程
                }
            }
            public string Dequeue()
            {
                lock (_messages)
                {
                    while (_messages.Count == 0)
                    {
                        Monitor.Wait(_messages); // 释放锁并等待
                    }
                    return _messages.Dequeue();
                }
            }
        }
    }

    //Monitor.IsEntered
}