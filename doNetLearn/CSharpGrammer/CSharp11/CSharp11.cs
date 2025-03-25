using BenchmarkDotNet.Portability;
using NPOI.SS.Formula.Functions;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace doNetLearn.CSharpGrammer
{

    public interface ICSharp11
    {
        /// <summary>
        /// 2. 泛型数学支持 , 泛型数学支持使泛型类型能够进行算术运算。这对于需要对不同数值类型进行操作的数学库或算法特别有用。
        /// </summary>
        public static virtual T Average<T>(T x, T y) where T : INumber<T>
        {
            return (x + y) / T.Create(2);
        }
    }


    /// <summary>
    ///  3.泛型特性 , C# 11允许使用泛型参数定义特性，这使得特性更具可复用性且类型安全。
    ///  优点：减少了为不同类型创建多个特性的需求。
    ///  缺点：增加了设计特性逻辑的复杂性。
    ///  实际应用场景：一个日志记录特性，它根据方法的返回类型以不同方式记录方法。
    /// </summary>
    public class ValidateTypeAttribute<T> : Attribute
    {
        public string ErrorMessage { get; }
        public ValidateTypeAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }



   


    public class CSharp11: ICSharp11
    {

        [ValidateType<string>()]
        public string Method() => default;


        // 原始字符串字面量
        //泛型数学支持
        //泛型属性
        //UTF-8 字符串字面量
        //字符串内插表达式中的换行符
        //列表模式
        //文件本地类型
        //必需的成员
        //自动默认结构
        //常量 string 上的模式匹配 Span<char>
        //扩展的 nameof 范围
        //数值 IntPtr
        //ref 字段和 scoped ref
        //改进了方法组向委托的转换
        //警告波 7




        /// <summary>
        /// 1. 原始字符串字面量 , 原始字符串字面量使得处理多行字符串更为简便，无需对特殊字符进行转义，也不用担心缩进问题。
        /// 优点：更易于维护格式化的字符串，例如JSON、XML和SQL查询
        /// 缺点：对于非常大的文本块，管理起来可能会变得困难
        /// 实际应用场景：存储HTML电子邮件的模板或配置数据，在这些场景中，保持精确的格式至关重要
        /// </summary>
        public static void Method1()
        {
            string sqlQuery = """
                                SELECT * FROM Users
                                WHERE Age > 25
                                ORDER BY LastName;
                                """;
            string jsonData = """
                            {
                                "name": "John Doe",
                                "age": 30,
                                "city": "New York"
                            }
                            """;                    
        }

       
        public class Vector2D<T> where T : INumber<T>
        {
            public T X { get; }
            public T Y { get; }
            public Vector2D(T x, T y)
            {
                X = x;
                Y = y;
            }
            public T Magnitude() => T.Sqrt(X * X + Y * Y);
        }

        /*
      优缺点：
            优点：减少代码重复并提高性能。
            缺点：如果你不熟悉泛型约束，实现起来可能会有挑战性。
            实际应用场景：构建一个财务计算库，其中的方法需要针对不同用例支持decimal和double类型。
         */

            
        /// <summary>
        /// 4.UTF-8字符串字面量 , UTF-8字符串字面量有助于在处理UTF-8编码文本时优化内存使用。
        /// </summary>
        public static void Method2()
        {
            ReadOnlySpan<byte> utf8Message = "Hello, world!"u8; //直接处理UTF-8编码的字符串
            //将文本数据直接以UTF-8格式存储可以减少网络通信中的内存开销。
            var utf8Data = Encoding.UTF8.GetBytes("Some text data");
        }



        /// <summary>
        /// 5.必需成员 
        /// C# 11引入了必需成员的概念，允许你指定在创建对象时某些属性或字段必须进行初始化。
        /// 这对于不可变对象（其中某些属性必须在初始化期间设置）特别有用。
        /// 优点：增强了数据完整性，防止关键字段缺失。
        /// 缺点：为对象初始化增加了更多的样板代码。
        /// 实际应用场景：确保在创建配置对象时，始终具有诸如连接字符串、API密钥或用户数据等必需参数。
        /// </summary>
        public static void Method3()
        {
            // 使用方式：
            var user = new User { Name = "John Doe", Age = 30 }; // 有效
            //var user2 = new User { Name = "John Doe" }; // 错误：'Age'是必需的
        }


        /// <summary>
        /// 6.对常量字符串进行Span模式匹配
        /// 此功能允许将Span<char>直接与常量字符串进行模式匹配，这可以显著提高字符串处理和解析性能，
        /// 特别是在处理高性能应用程序（如解析器或编译器）时
        /// 
        /// 优点：减少内存分配，加快字符串比较速度
        /// 缺点：需要熟悉Span<char>以及注重性能的编程方式
        /// 实际应用场景：在实现解析器或命令行界面时，性能至关重要，且需要在不进行内存分配的情况下解析字符串
        /// </summary>
        public static void Method4()
        {
            ReadOnlySpan<char> command = "START_PROCESS";
            if (command is "START_PROCESS")
            {
                Console.WriteLine("Process started.");
            }
            else if (command is "STOP_PROCESS")
            {
                Console.WriteLine("Process stopped.");
            }
            //处理文本协议
            ReadOnlySpan<char> protocol = "HTTP/1.1";
            if (protocol is "HTTP/1.1")
            {
                Console.WriteLine("Handling HTTP/1.1 request");
            }
            else if (protocol is "HTTP/2")
            {
                Console.WriteLine("Handling HTTP/2 request");
            }         
        }

        /// <summary>
        /// 7.自动默认结构体 
        /// 借助自动默认结构体特性，C# 11会自动将结构体初始化为其默认值，在处理不需要特定初始化的结构体时，可使代码更简洁
        /// </summary>
        public struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        // 在C# 11中，无需手动设置默认值：
        Point p = new(); // X和Y被初始化为0。

        public struct Circle
        {
            public double Radius { get; set; }
        }

        Circle circle = new(); // 半径自动设置为0。


        /****
         * 
        优缺点：
            优点：减少与未初始化字段相关的错误，减少样板代码。
            缺点：如果不希望自动设置默认值，可能会引入意外行为。
            实际应用场景：在图形应用程序中，将结构体用于像点、颜色或尺寸这样的简单数据结构。
         * 
         */

        /// <summary>
        /// 8.扩展的nameof作用域
        /// 在C# 11中，nameof运算符的作用域得到了扩展，允许它在更多场景中使用，
        /// 例如在特性或lambda表达式中。此功能通过改进重构能力，使代码更易于维护
        /// 优点：提供更好的重构支持，提高代码可读性
        /// 缺点：在不增加显著价值的上下文中可能会被误用
        /// 实际应用场景：使用nameof来确保验证逻辑中的属性名称与实际属性名称保持同步，以降低重构期间出现错误的风险
        /// </summary>
        public static void Method5()
        {
            //在lambda表达式中使用nameof
            Func<int, string> getName = (id) => $"{nameof(id)}: {id}";
        }

        //在特性中使用nameof
        [DisplayName(nameof(User.Name))]
        public string FirstName { get; set; }


        /// <summary>
        /// 9.数值型IntPtr
        /// C# 11中的数值型IntPtr允许更好地处理整数指针操作，特别是在涉及低级编程或与非托管代码进行互操作的场景中
        /// 优点：对于低级操作，代码更简洁，减少了类型转换
        /// 缺点：使用场景局限于涉及指针的情况
        /// 实际应用场景：游戏开发或与硬件交互的应用程序，在这些场景中，高效的内存操作至关重要
        /// </summary>
        public static void Method6()
        {
            //指针算术运算  在之前的版本中，IntPtr在进行算术运算时需要在int类型之间进行转换
            nint pointer = new nint(42);
            nint result = pointer + 2; // 现在可以直接进行算术运算。

            ////内存管理  这在访问内存映射文件或进行本机互操作等场景中很有用。
            //IntPtr baseAddress = ...;
            //IntPtr offsetAddress = baseAddress + 128;           
        }

        /// <summary>
        /// 10.ref字段和作用域ref
        /// C# 11引入了在结构体中声明ref字段的能力，通过引用现有数据而不复制数据，实现更高效的内存管理
        /// </summary>
        public struct BufferWrapper
        {
            ////结构体中的ref字段    在之前的版本中，这需要诸如使用指针或不安全代码之类的变通方法。
            //private ref int _value;

            //public BufferWrapper(ref int value)
            //{
            //    _value = ref value;
            //}
        }

        /// <summary>
        /// 作用域ref参数
        /// </summary>
        /// <param name="value"></param>
        public void ModifyValue(scoped ref int value)
        {
            value *= 2;
        }
        /*
         * 
        优缺点：
            优点：通过避免不必要的复制来提高性能。
            缺点：增加了复杂性，特别是在理解ref语义方面。
            实际应用场景：高性能数据处理，例如在内存中操作大型数据集且无需复制的自定义数据结构。
         * 
         */





        //11.改进的方法组到委托的转换

        /// <summary>
        /// 事件处理程序   在之前的版本中，你可能需要手动将HandleEvent转换为Action。
        /// </summary>
        public class EventHandlerExample
        {
            public event Action OnEvent;

            public void Initialize()
            {
                OnEvent += HandleEvent;
            }

            private void HandleEvent() { /*... */ }


            //简化LINQ查询
            //var numbers = new[] { 1, 2, 3, 4, 5 };
            //var squares = numbers.Select(Math.Pow);

            /*
             * 
           优缺点：
                优点：代码更简洁、更具可读性。
                缺点：如果过度使用，可能会掩盖方法细节。
                实际应用场景：注册事件处理程序或在LINQ操作中直接使用现有方法 
             * 
             */
        }

        /// <summary>
        /// 13.字符串插值表达式中的换行  此功能允许你在字符串插值块中使用换行符，使复杂的插值更具可读性
        /// 优点：提高复杂字符串插值的可读性。
        /// 缺点：可能会被过度使用，导致代码杂乱。
        /// 实际应用场景：创建动态电子邮件模板或详细的日志消息。
        /// </summary>
        public static void Method13()
        {
            var user = new User { Name = "John Doe", Age = 30 }; // 有效
            Console.WriteLine($"""
            The user {user.Name} has logged in.
            Role: {user.Role}
            Last login: {user.LastLogin}
            """);

            string emailContent = $"""
            Hi {user.FirstName},

            Welcome to our service. Your account is now active.

            Regards,
            Team
            """;
        }
    }


    /// <summary>
    /// 12.文件局部类型 ,文件局部类型允许你将类型的作用域限制在其定义所在的文件内 
    /// </summary>
    file class LoggerHelper
    {
        public static void Log(string message) => Console.WriteLine(message);
    }

    file struct Vector3D { /*...*/ }


    public class User
    {
        public required string Name { get; init; }
        public required int Age { get; init; }
        public string Role { get; set; }
        public DateTime LastLogin { get; set; }
        public string FirstName { get; set; }
    }

    /// <summary>
    /// required 这有助于确保始终提供必要的设置，防止运行时出现问题
    /// </summary>
    public class AppSettings
    {
        public required string DatabaseConnection { get; init; }
        public required string ApiKey { get; init; }
    }


       
    /// <summary>
    /// 列表模式允许对列表或数组进行模式匹配，从而更易于检查集合中的特定结构
    /// </summary>
    public class PatternCls
    {
        /// <summary>
        /// 匹配特定模式
        /// </summary>
        public void Method()
        {
            //匹配特定模式
            int[] numbers = { 1, 2, 3 };
            if (numbers is [1, 2, 3])
            {
                Console.WriteLine("The array contains 1, 2, and 3.");
            }

            //检测前缀
            if (numbers is [1, ..])
            {
                Console.WriteLine("The array starts with 1.");
            }
        }
    }
}
