using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchString.ConcurrentFiles
{
    internal class PartitionerDemo
    {
        /// <summary>
        /// 简单的分区器，将以线程安全的方式从底层集合中一次提取一个项目。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        class SingleElementPartitioner<T> : Partitioner<T>
        {
            // 此分区器正在包装的集合
            IEnumerable<T> m_referenceEnumerable;

            // 用作底层集合的共享枚举的内部类。
            private class InternalEnumerable : IEnumerable<T>, IDisposable
            {
                IEnumerator<T> m_reader;
                bool m_disposed = false;

                //这两个函数用于在执行静态分区时实现Dispose（）
                int m_activeEnumerators;
                bool m_downcountEnumerators;

                // “downcountEnumerators”对于静态分区为true，对于动态分区为false。
                public InternalEnumerable(IEnumerator<T> reader, bool downcountEnumerators)
                {
                    m_reader = reader;
                    m_activeEnumerators = 0;
                    m_downcountEnumerators = downcountEnumerators;
                }

                public IEnumerator<T> GetEnumerator()
                {
                    if (m_disposed)
                        throw new ObjectDisposedException("InternalEnumerable: Can't call GetEnumerator() after disposing");

                    // 对于静态分区，请跟踪活动枚举器的数量。
                    if (m_downcountEnumerators) Interlocked.Increment(ref m_activeEnumerators);

                    return new InternalEnumerator(m_reader, this);
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return ((IEnumerable<T>)this).GetEnumerator();
                }

                public void Dispose()
                {
                    if (!m_disposed)
                    {
                        // 只有在进行动态分区时才处理源枚举器
                        if (!m_downcountEnumerators)
                        {
                            m_reader.Dispose();
                        }
                        m_disposed = true;
                    }
                }

                // 从生成的InternalEnumerator的Dispose（）方法调用。
                // 在静态分区过程中，一旦所有请求的InternalEnumerators都被释放，源枚举器将自动被释放。                  
                public void DisposeEnumerator()
                {
                    if (m_downcountEnumerators)
                    {
                        if (Interlocked.Decrement(ref m_activeEnumerators) == 0)
                        {
                            m_reader.Dispose();
                        }
                    }
                }
            }

            //用作基础集合的共享枚举器的内部类。            
            private class InternalEnumerator : IEnumerator<T>
            {
                T m_current;
                IEnumerator<T> m_source;
                InternalEnumerable m_controllingEnumerable;
                bool m_disposed = false;

                public InternalEnumerator(IEnumerator<T> source, InternalEnumerable controllingEnumerable)
                {
                    m_source = source;
                    m_current = default(T);
                    m_controllingEnumerable = controllingEnumerable;
                }

                object IEnumerator.Current
                {
                    get { return m_current; }
                }

                T IEnumerator<T>.Current
                {
                    get { return m_current; }
                }

                void IEnumerator.Reset()
                {
                    throw new NotSupportedException("Reset() not supported");
                }

                // This method is the crux of this class.  Under lock, it calls
                // MoveNext() on the underlying enumerator and grabs Current.
                bool IEnumerator.MoveNext()
                {
                    bool rval = false;
                    lock (m_source)
                    {
                        rval = m_source.MoveNext();
                        m_current = rval ? m_source.Current : default(T);
                    }
                    return rval;
                }

                void IDisposable.Dispose()
                {
                    if (!m_disposed)
                    {
                        // Delegate to parent enumerable's DisposeEnumerator() method
                        m_controllingEnumerable.DisposeEnumerator();
                        m_disposed = true;
                    }
                }
            }

            // Constructor just grabs the collection to wrap
            public SingleElementPartitioner(IEnumerable<T> enumerable)
            {
                // Verify that the source IEnumerable is not null
                if (enumerable == null)
                    throw new ArgumentNullException("enumerable");

                m_referenceEnumerable = enumerable;
            }

            // Produces a list of "numPartitions" IEnumerators that can each be
            // used to traverse the underlying collection in a thread-safe manner.
            // This will return a static number of enumerators, as opposed to
            // GetDynamicPartitions(), the result of which can be used to produce
            // any number of enumerators.
            public override IList<IEnumerator<T>> GetPartitions(int numPartitions)
            {
                if (numPartitions < 1)
                    throw new ArgumentOutOfRangeException("NumPartitions");

                List<IEnumerator<T>> list = new List<IEnumerator<T>>(numPartitions);

                // Since we are doing static partitioning, create an InternalEnumerable with reference
                // counting of spawned InternalEnumerators turned on.  Once all of the spawned enumerators
                // are disposed, dynamicPartitions will be disposed.
                var dynamicPartitions = new InternalEnumerable(m_referenceEnumerable.GetEnumerator(), true);
                for (int i = 0; i < numPartitions; i++)
                    list.Add(dynamicPartitions.GetEnumerator());

                return list;
            }

            // Returns an instance of our internal Enumerable class.  GetEnumerator()
            // can then be called on that (multiple times) to produce shared enumerators.
            public override IEnumerable<T> GetDynamicPartitions()
            {
                // Since we are doing dynamic partitioning, create an InternalEnumerable with reference
                // counting of spawned InternalEnumerators turned off.  This returned InternalEnumerable
                // will need to be explicitly disposed.
                return new InternalEnumerable(m_referenceEnumerable.GetEnumerator(), false);
            }

            // Must be set to true if GetDynamicPartitions() is supported.
            public override bool SupportsDynamicPartitions
            {
                get { return true; }
            }
        }

        class Program
        {
            // Test our SingleElementPartitioner(T) class
            static void Main()
            {
                // Our sample collection
                string[] collection = new string[] {"red", "orange", "yellow", "green", "blue", "indigo",
                "violet", "black", "white", "grey"};

                // Instantiate a partitioner for our collection
                SingleElementPartitioner<string> myPart = new SingleElementPartitioner<string>(collection);

                //
                // Simple test with ForEach
                //
                Console.WriteLine("Testing with Parallel.ForEach");
                Parallel.ForEach(myPart, item =>
                {
                    Console.WriteLine("  item = {0}, thread id = {1}", item, Thread.CurrentThread.ManagedThreadId);
                });

                //
                //
                // Demonstrate the use of static partitioning, which really means
                // "using a static number of partitioners".  The partitioners themselves
                // may still be "dynamic" in the sense that their outputs may not be
                // deterministic.
                //
                //

                // Perform static partitioning of collection
                var staticPartitions = myPart.GetPartitions(2);
                int index = 0;

                Console.WriteLine("Static Partitioning, 2 partitions, 2 tasks:");

                // Action will consume from static partitions
                Action staticAction = () =>
                {
                    int myIndex = Interlocked.Increment(ref index) - 1; // compute your index
                    var myItems = staticPartitions[myIndex]; // grab your static partition
                    int id = Thread.CurrentThread.ManagedThreadId; // cache your thread id

                    // Enumerate through your static partition
                    while (myItems.MoveNext())
                    {
                        Thread.Sleep(50); // guarantees that multiple threads have a chance to run
                        Console.WriteLine("  item = {0}, thread id = {1}", myItems.Current, Thread.CurrentThread.ManagedThreadId);
                    }

                    myItems.Dispose();
                };

                // Spawn off 2 actions to consume 2 static partitions
                Parallel.Invoke(staticAction, staticAction);

                //
                //
                // Demonstrate the use of dynamic partitioning
                //
                //

                // Grab an IEnumerable which can then be used to generate multiple
                // shared IEnumerables.
                var dynamicPartitions = myPart.GetDynamicPartitions();

                Console.WriteLine("Dynamic Partitioning, 3 tasks:");

                // Action will consume from dynamic partitions
                Action dynamicAction = () =>
                {
                    // Grab an enumerator from the dynamic partitions
                    var enumerator = dynamicPartitions.GetEnumerator();
                    int id = Thread.CurrentThread.ManagedThreadId; // cache our thread id

                    // Enumerate through your dynamic enumerator
                    while (enumerator.MoveNext())
                    {
                        Thread.Sleep(50); // guarantees that multiple threads will have a chance to run
                        Console.WriteLine("  item = {0}, thread id = {1}", enumerator.Current, id);
                    }

                    enumerator.Dispose();
                };

                // Spawn 3 concurrent actions to consume the dynamic partitions
                Parallel.Invoke(dynamicAction, dynamicAction, dynamicAction);

                // Clean up
                if (dynamicPartitions is IDisposable)
                    ((IDisposable)dynamicPartitions).Dispose();
            }
        }
    }
}
