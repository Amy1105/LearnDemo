using NPOI.SS.Formula.Functions;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.DataStructures
{
    /// <summary>
    /// c#中各种数据结构
    /// </summary>
    internal class TestMethod
    {
        //Dictionary<TKey, TValue>
        //List<T>

        //KeyValuePair


        /// <summary>
        /// HashSet<T> 基于哈希表实现的高性能无序集合，专门用于存储唯一元素并提供接近O(1)时间复杂度的查找操作
        /// 特性：
        /// 唯一性 自动去重，不允许重复元素
        /// 无序存储    元素不按插入顺序或值排序
        /// 高效查找    基于哈希表，查找/插入/删除平均时间复杂度为 O(1)
        /// 线程不安全  多线程环境下需手动同步（或使用ConcurrentHashSet第三方库）
        /// Null值支持 允许单个null元素（若泛型T为引用类型）
        /// 
        /// 注意事项：
        /// 1.哈希冲突影响性能
        /// 2.遍历顺序不可预测
        /// 3.不适合频繁修改的排序需求
        /// </summary>
        public void HashSetMethod()
        {
            //场景1：快速去重
            var duplicates = new List<int> { 1, 2, 2, 3, 3, 3 };
            var unique = new HashSet<int>(duplicates); // 结果: {1, 2, 3}

            //场景2：高效成员检查
            string userInput = "A1";
            var validCodes = new HashSet<string> { "A1", "B2", "C3" };
            bool isValid = validCodes.Contains(userInput); // 比List.Contains快得多

            //场景3：集合运算
            var setA = new HashSet<int> { 1, 2, 3 };
            var setB = new HashSet<int> { 2, 3, 4 };
            setA.IntersectWith(setB); // setA变为 {2, 3}
        }



        /// <summary>
        /// LinkedList<T> 基于双向链表实现的线性集合，允许在O(1)时间复杂度内进行高效的插入和删除操作
        /// 
        /// 注意事项：
        /// 1.节点引用有效性，LinkedListNode<T>被删除后，其Next/Previous属性变为null
        /// 2.自定义相等比较，Find()和Contains()依赖Equals()方法，自定义类型需重写
        /// 3.批量操作性能，大量数据建议用List<T>先处理，再转换为链表
        /// 
        /// </summary>
        public void LinkedListMethod()
        {
            //LinkedListNode<T>

            // 创建链表
            var linkedList = new LinkedList<string>();

            // 在尾部添加元素
            linkedList.AddLast("Apple");
            linkedList.AddLast("Banana");

            // 在头部添加元素
            linkedList.AddFirst("Orange");

            // 在指定节点后插入
            var node = linkedList.Find("Apple");
            linkedList.AddAfter(node, "Peach");

            // 顺序遍历
            foreach (var fruit in linkedList)
            {
                Console.WriteLine(fruit); // 输出: Orange, Apple, Peach, Banana
            }

            // 逆向遍历
            for (var node1 = linkedList.Last; node1 != null; node1 = node1.Previous)
            {
                Console.WriteLine(node1.Value); // 输出: Banana, Peach, Apple, Orange
            }

            //场景2：实现撤销/重做功能
            var undoStack = new LinkedList<string>();
            var redoStack = new LinkedList<string>();

            // 用户操作记录
            undoStack.AddLast("Add Text");
            undoStack.AddLast("Bold Text");

            // 撤销操作
            var lastAction = undoStack.Last.Value;
            undoStack.RemoveLast();
            redoStack.AddLast(lastAction);


            //场景3：环形缓冲区
            var circularBuffer = new LinkedList<int>();
            circularBuffer.AddLast(1);
            circularBuffer.AddLast(2);

            // 模拟环形访问
            var current = circularBuffer.First;
            while (true)
            {
                Console.WriteLine(current.Value);
                current = current.Next ?? circularBuffer.First; // 到达尾部则回到头部
            }

        }

        //场景1：高频插入/删除（如LRU缓存）
        // LRU缓存实现（最近最少使用）
        public class LRUCache<TKey, TValue>
        {
            private readonly LinkedList<(TKey Key, TValue Value)> _list = new();
            private readonly Dictionary<TKey, LinkedListNode<(TKey, TValue)>> _dict = new();
            private readonly int _capacity;

            public TValue Get(TKey key)
            {
                if (_dict.TryGetValue(key, out var node))
                {
                    _list.Remove(node);
                    _list.AddFirst(node); // 移动到头部表示最近使用
                    return node.Value.Value;
                }
                return default;
            }

            public void Add(TKey key, TValue value)
            {
                if (_dict.Count >= _capacity)
                {
                    var last = _list.Last;
                    _dict.Remove(last.Value.Key);
                    _list.RemoveLast(); // 淘汰最久未使用的
                }
                var newNode = _list.AddFirst((key, value));
                _dict.Add(key, newNode);
            }
        }

        /// <summary>
        /// PriorityQueue<TElement,TPriority>,.NET 6引入的优先级队列实现，基于最小堆（Min-Heap）数据结构，允许元素按优先级高效出队
        /// 
        /// 注意事项：
        /// 1.相等优先级的顺序，相同优先级的元素出队顺序不确定（非先进先出）
        /// 2.线程安全替代方案，多线程环境建议封装锁机制，或使用ConcurrentPriorityQueue第三方库
        /// 3.动态优先级更新，原生不支持直接修改优先级，需自行实现
        /// </summary>
        public void PriorityQueueMethod()
        {
            //场景1：任务调度
            var tasks = new PriorityQueue<string, int>();
            tasks.Enqueue("常规日志清理", 2);
            tasks.Enqueue("数据库备份", 1); // 最高优先级
            tasks.Enqueue("用户报告生成", 3);

            while (tasks.TryDequeue(out var task, out _))
            {
                Console.WriteLine($"执行: {task}");
            }
            // 输出顺序：数据库备份 → 常规日志清理 → 用户报告生成

            //场景2：Dijkstra算法（最短路径）
            var pq = new PriorityQueue<(int Node, int Dist), int>();
            pq.Enqueue((startNode, 0), 0); // 按距离排序

            while (pq.Count > 0)
            {
                var (node, dist) = pq.Dequeue();
                // 处理邻接节点...
            }

            //场景3：自定义优先级规则
            // 自定义比较器（实现最大优先队列）
            var maxQueue = new PriorityQueue<string, int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));
            maxQueue.Enqueue("A", 1);
            maxQueue.Enqueue("B", 3); // 优先级更高的先出队
            Console.WriteLine(maxQueue.Dequeue()); // 输出 "B"

            //批量入队（使用集合初始化器）  有问题
            //var queue = new PriorityQueue<string, int>
            //{
            //    ("TaskA", 3),
            //    ("TaskB", 1),
            //    ("TaskC", 2)
            //};
        }

        class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }
        public void Method2()
        {
            // 元素为自定义类，按Age属性作为优先级
            var queue = new PriorityQueue<Person, int>();
            queue.Enqueue(new Person { Name = "Alice", Age = 25 }, 25);
            queue.Enqueue(new Person { Name = "Bob", Age = 30 }, 30);
            var oldest = queue.Dequeue(); // Bob（优先级更高）
        }


        /// <summary>
        /// SortedDictionary<TKey,TValue>
        /// 是一个基于**红黑树（Red-Black Tree）**实现的有序键值对集合，它根据键自动维护元素的排序状态，并提供高效的查找、插入和删除操作
        /// 插入/删除/查找操作均为 O(log n)
        /// 
        /// 注意事项：
        /// 1.键的不可变性
        /// 2.自定义键类型，需实现IComparable<T>或提供IComparer<T>
        /// 3.批量插入性能，大量数据建议先填充后排序（或用Dictionary转换）
        /// </summary>
        public void SortedDictionaryMethod()
        {
            // 按字符串长度降序
            var customSortedDict = new SortedDictionary<string, int>(
                Comparer<string>.Create((x, y) => y.Length.CompareTo(x.Length))
            )
            {
                ["Apple"] = 1,
                ["Peach"] = 2,
                ["Watermelon"] = 3 // 最长键排在最前
            };

            //场景1：维护有序键值数据
            var studentScores = new SortedDictionary<string, int>
            {
                ["Alice"] = 90,
                ["Bob"] = 85,
                ["Charlie"] = 95
            };
            // 自动按姓名排序输出
            foreach (var (name, score) in studentScores)
            {
                Console.WriteLine($"{name}: {score}");
            }

            // 场景2：范围查询
            // 获取键在 "B" 到 "D" 之间的条目          
            var range = studentScores.Keys
                .SkipWhile(k => k.CompareTo("B") < 0)
                .TakeWhile(k => k.CompareTo("D") <= 0);

            //场景3：替代SortedSet的扩展功能
            // 使用值存储额外信息（如出现次数）
            var words = new string[] { };
            var wordCounts = new SortedDictionary<string, int>();
            foreach (var word in words)
            {
                wordCounts[word] = wordCounts.TryGetValue(word, out int count) ? count + 1 : 1;
            }
        }


        /// <summary>
        /// SortedList<TKey,TValue>
        /// 基于动态数组实现的有序键值对集合，它根据键自动维护元素的排序状态，并提供高效的查找操作
        /// 
        /// 特性：
        /// 基于数组实现，比SortedDictionary占用更少内存
        /// 查找：O(log n)；插入/删除：O(n)
        /// 
        /// 注意事项：
        /// 1.插入/删除性能，频繁增删时优先选择SortedDictionary或Dictionary
        /// 2.键的不可变性，键对象应不可变，或避免修改后影响排序结果。
        /// 3.自定义键类型,需实现IComparable<T>或提供IComparer<T>
        /// 4.初始化容量优化,预先设置容量避免动态扩容
        /// 
        /// </summary>
        public void SortedListMethod()
        {
            //场景1：维护有序且需索引访问的数据
            var products = new SortedList<int, string>
            {
                { 1003, "Laptop" },
                { 1001, "Phone" },  // 自动按键排序：1001, 1003
                { 1002, "Tablet" }
            };
            // 通过索引访问
            string firstProduct = products.Values[0]; // "Phone"

            //场景2：范围查询
            // 获取键在1002到1004之间的元素
            var range = products
                .Where(kvp => kvp.Key >= 1002 && kvp.Key <= 1004)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            //场景3：自定义排序规则
            // 按字符串长度降序
            var customSorted = new SortedList<string, int>(Comparer<string>.Create((x, y) => y.Length.CompareTo(x.Length)))
            {
                { "Apple", 1 },
                { "Peach", 2 },
                { "Watermelon", 3 } // 最长键排在最前
            };


//与SortedDictionary的区别
//特性            SortedList<K, V>        SortedDictionary<K, V>
//底层结构            动态数组                   红黑树
//插入/删除性能       较差（需移动元素）	        更优（树结构调整）
//内存占用            更低（连续存储）	            较高（节点开销）
//索引访问            支持                       不支持
//适用场景          少量数据或频繁查找/极少修改    频繁增删

        }


        public void Method1()
        {
            var text = "apple banana apple orange banana apple";
            var words = text.Split(' ');
            var wordCounts = new SortedList<string, int>();
            foreach (var word in words)
            {
                wordCounts[word] = wordCounts.TryGetValue(word, out int count) ? count + 1 : 1;
            }
            // 输出按字母排序的结果
            foreach (var (word, count) in wordCounts)
            {
                Console.WriteLine($"{word}: {count}");
            }
            // 输出：
            // apple: 3
            // banana: 2
            // orange: 1
        }


        //操作    SortedSet<T>    List<T> + Sort      HashSet<T>
        //插入     O(log n)           O(1)*	            O(1)
        //删除     O(log n)           O(n)               O(1)
        //查找     O(log n)           O(log n)**	         O(1)
        //有序遍历 O(n)                O(n)                无序

        /// <summary>
        /// SortedSet<T>   排序且不重复的数组,实现的是List<T> + Sort的功能，性能更好
        ///  
        /// 使用需要注意点：
        /// 1.自定义对象排序
        /// 2.与LINQ的兼容性，SortedSet<T>支持LINQ，但部分操作（如OrderBy）会失去树结构的性能优势
        /// 3.内存开销 比List<T>占用更多内存（每个节点需存储左右子树指针）
        /// 
        /// 
        /// 
        /// </summary>
        public void SortedSetMethod()
        {
            //场景1：维护有序唯一数据
            var uniqueWords = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);
            uniqueWords.Add("Apple");
            uniqueWords.Add("banana");
            uniqueWords.Add("apple"); // 忽略大小写，不会重复添加
                                      // 输出：Apple, banana

            //场景2：高效范围查询
            var numbers = new SortedSet<int> { 10, 20, 30, 40, 50 };
            var range = numbers.GetViewBetween(20, 40); // 获取20到40之间的元素
                                                        // 结果：20, 30, 40

            //场景3：实现优先队列（最小堆）
            var minHeap = new SortedSet<int>();
            minHeap.Add(5); 
            minHeap.Add(2);
            minHeap.Add(8);
            int min = minHeap.Min; // 获取最小值2
            minHeap.Remove(min);   // 弹出最小值
        }

        //Queue<T>
        public void QueueMethod()
        {
            ////场景1：任务调度系统
            //var taskQueue = new Queue<string>();
            //taskQueue.Enqueue("处理用户订单");
            //taskQueue.Enqueue("生成报表");
            //taskQueue.Enqueue("发送通知");

            //while (taskQueue.TryDequeue(out var task))
            //{
            //    Console.WriteLine($"正在执行: {task}");
            //}

            // 输出顺序：处理用户订单 → 生成报表 → 发送通知

            ////场景2：BFS算法（广度优先搜索）
            //var queue = new Queue<TreeNode>();
            //queue.Enqueue(rootNode);

            //while (queue.Count > 0)
            //{
            //    var node = queue.Dequeue();
            //    foreach (var child in node.Children)
            //    {
            //        queue.Enqueue(child); // 子节点入队
            //    }
            //}

            ////场景3：消息缓冲
            //var messageQueue = new Queue<Message>();
            //// 生产者线程
            //Task.Run(() => {
            //    while (true)
            //    {
            //        messageQueue.Enqueue(GetMessage());
            //        Thread.Sleep(100);
            //    }
            //});

            //// 消费者线程
            //Task.Run(() => {
            //    while (true)
            //    {
            //        if (messageQueue.TryDequeue(out var msg))
            //        {
            //            ProcessMessage(msg);
            //        }
            //    }
            //});

            //性能优化
            //1.预设初始容量（避免频繁扩容）
            //var optimizedQueue = new Queue<int>(capacity: 1000);
            //2.批量出队处理
            //var batchSize = 10;
            //while (queue.Count > 0)
            //{
            //    var batch = new List<string>();
            //    for (int i = 0; i < batchSize && queue.Count > 0; i++)
            //    {
            //        batch.Add(queue.Dequeue());
            //    }
            //    ProcessBatch(batch);
            //}
        }
        

        /// <summary>
        /// Stack<T> 基于数组实现的后进先出（LIFO）集合，专为需要反向处理数据的场景设计
        /// </summary>
        public void StackMethod()
        {
            //场景1：撤销（Undo）功能实现
            var undoStack = new Stack<string>();
            var document = "原始内容";

            // 用户操作记录
            undoStack.Push(document);
            document = "第一次修改";
            undoStack.Push(document);

            // 撤销操作
            document = undoStack.Pop(); // 恢复为"原始内容"

            //场景2：括号匹配检查
            bool IsValid(string s)
            {
                var stack = new Stack<char>();
                foreach (var c in s)
                {
                    if (c == '(') stack.Push(')');
                    else if (c == '[') stack.Push(']');
                    else if (c == '{') stack.Push('}');
                    else if (stack.Count == 0 || stack.Pop() != c)
                        return false;
                }
                return stack.Count == 0;
            }

            //场景3：DFS算法（深度优先搜索）
            //var stack = new Stack<TreeNode>();
            //stack.Push(rootNode);

            //while (stack.Count > 0)
            //{
            //    var node = stack.Pop();
            //    foreach (var child in node.Children.Reverse()) // 保持顺序需反转
            //    {
            //        stack.Push(child);
            //    }
            //}

            //性能优化
            //1.预设初始容量 
            //var optimizedStack = new Stack<int>(capacity: 1000); // 避免初期扩容

            //2.批量出栈处理
            //while (stack.Count > 0)
            //{
            //    var batch = new List<string>();
            //    for (int i = 0; i < 10 && stack.Count > 0; i++)
            //    {
            //        batch.Add(stack.Pop());
            //    }
            //    ProcessBatch(batch);
            //}



        }
    }
}
