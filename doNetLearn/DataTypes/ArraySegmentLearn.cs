using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.DataTypes
{
    /// <summary>
    ///  ArraySegment<T> 结构  , 分隔一维数组的一部分
    ///  ArraySegment<T> 是数组的包装器，用于分隔该数组中的元素范围。 
    ///  多个 ArraySegment<T> 实例可以引用相同的原始数组，并且可以重叠。 原始数组必须是一维数组，并且必须具有从零开始的索引
    /// </summary>
    internal class ArraySegmentLearn
    {
        public static void Method()
        {
           
            string[] myArr = { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };

            // 显示数组的初始内容。
            Console.WriteLine("The original array initially contains:");
            PrintIndexAndValues(myArr);

            // 定义一个包含整个数组的数组段。
            ArraySegment<string> myArrSegAll = new ArraySegment<string>(myArr);

            // 显示ArraySegment的内容。
            Console.WriteLine("The first array segment (with all the array's elements) contains:");
            PrintIndexAndValues(myArrSegAll);

            // 定义一个包含数组中间五个值的数组段。
            ArraySegment<string> myArrSegMid = new ArraySegment<string>(myArr, 2, 5);

            // 显示ArraySegment的内容。
            Console.WriteLine("The second array segment (with the middle five elements) contains:");
            PrintIndexAndValues(myArrSegMid);

            // 修改第一个数组段myArrSegAll的第四个元素。
            if (myArrSegAll.Array != null)
            {
                myArrSegAll.Array[3] = "LION";
            }
            // 显示第二个数组段myArrSegMid的内容。
            // 请注意，其第二个元素的值也发生了变化。
            Console.WriteLine("After the first array segment is modified, the second array segment now contains:");
            PrintIndexAndValues(myArrSegMid);
        }

        public static void PrintIndexAndValues(ArraySegment<string> arrSeg)
        {
            if (arrSeg.Array != null)
            {
                for (int i = arrSeg.Offset; i < (arrSeg.Offset + arrSeg.Count); i++)
                {
                    Console.WriteLine("   [{0}] : {1}", i, arrSeg.Array[i]);
                }
            }
            Console.WriteLine();
        }

        public static void PrintIndexAndValues(String[] myArr)
        {
            for (int i = 0; i < myArr.Length; i++)
            {
                Console.WriteLine(" [{0}] : {1}", i, myArr[i]);
            }
            Console.WriteLine();
        }



        private const int segmentSize = 10;

        /// <summary>
        /// 可以将仅表示数组的一部分作为参数的 ArraySegment<T> 对象传递给方法，而不是调用相对昂贵的方法（如 Copy）来传递数组的一部分副本。
        /// 在多线程应用中，可以使用 ArraySegment<T> 结构让每个线程只对数组的一部分进行操作。
        /// 对于基于任务的异步操作，可以使用 ArraySegment<T> 对象来确保每个任务对数组的不同段进行操作。 
        /// 
        /// 以下示例将数组划分为包含最多 10 个元素的单个段。 段中的每个元素都乘以其段号。 
        /// 结果显示，使用 ArraySegment<T> 类以这种方式操作元素会更改其基础数组的值。
        /// </summary>
        /// <returns></returns>
        public  async Task Method2()
        {
            List<Task> tasks = new List<Task>();

            // Create array.
            int[] arr = new int[50];
            // GetUpperBound() 获取数组中指定维度的最后一个元素的索引。
            for (int ctr = 0; ctr <= arr.GetUpperBound(0); ctr++)
                arr[ctr] = ctr + 1;

            // 以10段为单位处理数组。
            // Math.Ceiling() 返回大于或等于指定值的最小整数值双精度浮点数。
            for (int ctr = 1; ctr <= Math.Ceiling(((double)arr.Length) / segmentSize); ctr++)
            {
                int multiplier = ctr;
                int elements = (multiplier - 1) * 10 + segmentSize > arr.Length ?
                                arr.Length - (multiplier - 1) * 10 : segmentSize;
                ArraySegment<int> segment = new ArraySegment<int>(arr, (ctr - 1) * 10, elements);
                tasks.Add(Task.Run(() => {
                    IList<int> list = (IList<int>)segment;
                    for (int index = 0; index < list.Count; index++)
                        list[index] = list[index] * multiplier;
                }));
            }
            try
            {
                await Task.WhenAll(tasks.ToArray());
                int elementsShown = 0;
                foreach (var value in arr)
                {
                    Console.Write("{0,3} ", value);
                    elementsShown++;
                    if (elementsShown % 18 == 0)
                        Console.WriteLine();
                }
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Errors occurred when working with the array:");
                foreach (var inner in e.InnerExceptions)
                    Console.WriteLine("{0}: {1}", inner.GetType().Name, inner.Message);
            }
        }

        /// <summary>
        /// Equals 方法和相等运算符在比较两个 ArraySegment<T> 对象时测试引用相等性。 若要将两个 ArraySegment<T> 对象视为相等，它们必须满足以下所有条件：
        /// 1.引用相同的数组。
        /// 2.从数组中的同一索引开始。
        /// 3.具有相同数量的元素。
        /// 
        /// 
        /// 如果要在 ArraySegment<T> 对象中按其索引检索元素，则必须将其强制转换为 IList<T> 对象，并使用 IList<T>.Item[] 属性对其进行检索或修改。 
        /// 请注意，F# 中不需要这样做。
        /// </summary>
        public static void Method3()
        {
            String[] names = { "Adam", "Bruce", "Charles", "Daniel",
                         "Ebenezer", "Francis", "Gilbert",
                         "Henry", "Irving", "John", "Karl",
                         "Lucian", "Michael" };
            var partNames = new ArraySegment<string>(names, 2, 5);

            // 将ArraySegment对象转换为IList<string>并枚举它。
            var list = (IList<string>)partNames;
            for (int ctr = 0; ctr <= list.Count - 1; ctr++)
                Console.WriteLine(list[ctr]);
        }
    }
}
