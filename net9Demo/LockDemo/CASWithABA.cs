using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace net9Demo.LockDemo
{
    /// <summary>
    /// .NET 中解决 CAS 的 ABA 问题
    /// </summary>
    internal class CASWithABA
    {
        /// <summary>
        /// 1.使用版本号/时间戳（推荐方案） 
        /// 这是最常用的解决方案，通过为每个值关联一个版本号或时间戳来检测中间变化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public struct VersionedReference<T> where T : class
        {
            private int version;
            private T value;

            public VersionedReference(T value)
            {
                this.value = value;
                this.version = 0;
            }

            public bool CompareExchange(T newValue, T expectedValue, int expectedVersion, out int newVersion)
            {
                // 模拟带版本号的CAS操作
                if (this.value == expectedValue && this.version == expectedVersion)
                {
                    this.value = newValue;
                    this.version++;
                    newVersion = this.version;
                    return true;
                }
                newVersion = this.version;
                return false;
            }

            public T Value => value;
            public int Version => version;
        }

        public void TestMethod1()
        {
            var reference = new VersionedReference<string>("初始值");

            // 线程1读取
            var oldValue = reference.Value;
            var oldVersion = reference.Version;

            // 线程2修改并改回
            reference.CompareExchange("修改值", "初始值", 0, out _);
            reference.CompareExchange("初始值", "修改值", 1, out _);

            // 线程1尝试修改
            bool success = reference.CompareExchange("新值", oldValue, oldVersion, out _);
            // success将为false，因为版本号已改变
        }


        /// <summary>
        /// 使用 .NET 的 ConditionalWeakTable
        /// </summary>
        public class ConditionalWeakTableDemo
        {
            private static readonly ConditionalWeakTable<object, VersionBox> versionTable = new ConditionalWeakTable<object, VersionBox>();

            private class VersionBox
            {
                public int Version { get; set; }
            }

            public static bool CompareExchangeWithVersion(ref object location, object newValue, object expectedValue)
            {
                var box = versionTable.GetOrCreateValue(expectedValue);
                int expectedVersion = box.Version;

                // 模拟原子操作
                if (ReferenceEquals(location, expectedValue))
                {
                    var newBox = versionTable.GetOrCreateValue(newValue);
                    newBox.Version = box.Version + 1;
                    location = newValue;
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 3.使用 System.Collections.Immutable 中的不可变集合
        /// </summary>
        public class Method3
        {
            /// <summary>
            /// 不可变集合天然避免ABA问题，因为每次修改都返回新实例：
            /// </summary>
            public void Test()
            {
                var list = ImmutableList<int>.Empty;
                var list1 = list.Add(1);  // 创建新实例
                var list2 = list1.Add(2); // 创建新实例
            }
        }

        /// <summary>
        /// 4. 使用 MemoryBarrier 和双重检查
        /// </summary>
        public class Method4
        {
            ///// <summary>
            ///// 伪代码
            ///// </summary>
            //public bool Test()
            //{
            //    object current = _value;
            //    Thread.MemoryBarrier();
            //    if (current == expected)
            //    {
            //        lock (_lock)
            //        {
            //            if (_value == expected)
            //            {
            //                _value = newValue;
            //                return true;
            //            }
            //        }
            //    }
            //    return false;
            //}
        }
    }
}
