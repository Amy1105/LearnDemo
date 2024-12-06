using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.DataTypes
{
    /// <summary>
    /// Memory<T> 和 Span<T> 使用准则
    /// 参考 https://learn.microsoft.com/zh-cn/dotnet/standard/memory-and-spans/memory-t-usage-guidelines
    /// </summary>
    internal class Class1
    {

        //.NET包含许多表示任意连续内存区域的类型。Span<T> 和ReadOnlySpan<T>是轻量级内存缓冲区，用于封装对托管或非托管内存的引用。
        //因为这些类型只能存储在堆栈上，所以它们不适合异步方法调用等场景。
        //为了解决这个问题。NET 2.1添加了一些其他类型，
        //包括 Memory<T>, ReadOnlyMemory<T>, IMemoryOwner<T>, and MemoryPool<T>。
        //与Span<T> 一样，Memory<T> 及其相关类型可以由托管和非托管内存支持。
        //与Span<T> 不同，Memory<T> 可以存储在托管堆上。


        //Span<T> 和Memory<T>都是可用于管道的结构化数据缓冲区的包装器。
        //也就是说，它们的设计使得部分或全部数据可以有效地传递给管道中的组件，这些组件可以处理它们并可选地修改缓冲区。
        //由于内存<T> 及其相关类型可以由多个组件或多个线程访问，因此遵循一些标准使用指南来生成健壮的代码非常重要。


        //理解：所有者、使用者和生存期管理 这三个概念

        //缓冲区可以在 API 之间传递，有时可以从多个线程访问，因此请注意如何管理缓冲区的生存期。 下面介绍三个核心概念：

        //所有权。 缓冲区实例的所有者负责生存期管理，包括在不再使用缓冲区时将其销毁。
        ////所有缓冲区都拥有一个所有者。 通常，所有者是创建缓冲区或从工厂接收缓冲区的组件。 
        ///所有权也可以转让；组件 A 可以将缓冲区的控制权转让给组件 B，此时组件 A 就无法再使用该缓冲区，组件 B 将负责在不再使用缓冲区时将其销毁。

        //使用。 允许缓冲区实例的使用者通过从中读取并可能写入其中来使用缓冲区实例。 缓冲区一次可以拥有一个使用者，除非提供了某些外部同步机制。 缓冲区的当前使用者不一定是缓冲区的所有者。

        //租用。 租用是允许特定组件成为缓冲区使用者的时长。








        // 理解： Memory<T> 和所有者/使用者模型

        //        支持单个所有权的模型。 缓冲区在其整个生存期内拥有单个所有者。

        //支持所有权转让的模型。 缓冲区的所有权可以从其原始所有者（其创建者）转让给其他组件，该组件随后将负责缓冲区的生存期管理。 该所有者可以反过来将所有权转让给其他组件等。


      static void Method()
        {
            //使用 System.Buffers.IMemoryOwner<T> 接口显式管理缓冲区的所有权
            //IMemoryOwner<T> 支持两种所有权模型。 具有 IMemoryOwner<T> 引用的组件拥有缓冲区
            IMemoryOwner<char> owner = MemoryPool<char>.Shared.Rent();

            //Method 方法保留对 IMemoryOwner<T> 实例的引用，因此 Method 方法是缓冲区的所有者。

            Console.Write("Enter a number: ");
            try
            {
                string? s = Console.ReadLine();

                if (s is null)
                    return;

                var value = Int32.Parse(s);

                var memory = owner.Memory;

                //WriteInt32ToBuffer 和 DisplayBufferToConsole 方法接受 Memory<T> 作为公共 API。
                //因此，它们是缓冲区的使用者。 这些方法一次使用一个缓冲区。

                WriteInt32ToBuffer(value, memory);

                DisplayBufferToConsole(owner.Memory.Slice(0, value.ToString().Length));
            }
            catch (FormatException)
            {
                Console.WriteLine("You did not enter a valid number.");
            }
            catch (OverflowException)
            {
                Console.WriteLine($"You entered a number less than {Int32.MinValue:N0} or greater than {Int32.MaxValue:N0}.");
            }
            finally
            {
                owner?.Dispose();
            }
        }

      public  static void WriteInt32ToBuffer(int value, Memory<char> buffer)
        {
            var strValue = value.ToString();

            var span = buffer.Span;
            for (int ctr = 0; ctr < strValue.Length; ctr++)
                span[ctr] = strValue[ctr];
        }

        public static void DisplayBufferToConsole(Memory<char> buffer) =>
            Console.WriteLine($"Contents of the buffer: '{buffer}'");


        /// <summary>
        /// 使用using编写如下
        /// </summary>
        static void Method2()
        {
            using (IMemoryOwner<char> owner = MemoryPool<char>.Shared.Rent())
            {
                Console.Write("Enter a number: ");
                try
                {
                    string? s = Console.ReadLine();

                    if (s is null)
                        return;

                    var value = Int32.Parse(s);

                    var memory = owner.Memory;
                    WriteInt32ToBuffer(value, memory);
                    DisplayBufferToConsole(memory.Slice(0, value.ToString().Length));
                }
                catch (FormatException)
                {
                    Console.WriteLine("You did not enter a valid number.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"You entered a number less than {Int32.MinValue:N0} or greater than {Int32.MaxValue:N0}.");
                }
            }
        }



        /// <summary>
        /// 无需使用 IMemoryOwner<T> 即可创建 Memory<T> 实例。 
        /// 在这种情况下，缓冲区的所有权是隐式的而不是显式的，并且仅支持单所有者模型
        /// </summary>
        static void Method3()
        {
            //直接调用 Memory<T> 构造函数之一   或   调用 String.AsMemory 扩展方法以生成 ReadOnlyMemory<char> 实例
            Memory<char> memory = new char[64];

            //最初创建 Memory<T> 实例的方法是缓冲区的隐式所有者。
            //无法将所有权转让给任何其他组件，因为没有 IMemoryOwner<T> 实例可用于进行转让。
            //（或者，也可以假设运行时的垃圾回收器拥有缓冲区，而所有方法仅使用缓冲区。）

            Console.Write("Enter a number: ");
            string? s = Console.ReadLine();

            if (s is null)
                return;

            var value = Int32.Parse(s);

            WriteInt32ToBuffer(value, memory);
            DisplayBufferToConsole(memory);
        }

       
    }

    //规则一：对于同步 API，如有可能，请使用 Span<T>（而不是 Memory<T>）作为参数
    ////1-Span<T> 比 Memory<T> 更通用，可以表示更多种类的连续内存缓冲区
    ////2-Span<T> 还提供比 Memory<T> 更好的性能
    ////3-尽管无法进行 Span<T> 到 Memory<T> 的转换，但可以使用 Memory<T>.Span 属性将 Memory<T> 实例转换为 Span<T>
    ///
    //规则二：如果缓冲区应为只读，则使用 ReadOnlySpan<T> 或 ReadOnlyMemory<T>

    //规则三：如果方法接受 Memory<T> 并返回void，则在方法返回之后不得使用 Memory<T> 实例。

    //规则四：如果方法接受 Memory<T> 并返回某个任务，则在该任务转换为终止状态之后不得使用 Memory<T>实例。

    //规则五：如果构造函数接受 Memory<T> 作为参数，则假定构造对象上的实例方法是 Memory<T> 实例的使用者


}
