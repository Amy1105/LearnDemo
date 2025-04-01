using System;
using System.Drawing;
using System.Globalization;
using System.Text.Json;

namespace doNetLearn.DataStructures
{
    /// <summary>
    /// ValueType值类型
    /// </summary>
    internal class ValueTypeDemo
    {
        /// <summary>
        /// Void   为不返回值的方法指定返回值类型。
        /// Nullable<T> 结构
        /// Byte  表示 8 位无符号整数
        /// Char 将字符表示为 UTF-16 代码单元
        /// Boolean   表示一个布尔（true 或 false）值
        /// Half 结构    .net5,6,7,8,9,
        /// Single 结构  表示单精度浮点数     
        /// Double  表示双精度浮点数
        /// Decimal 表示十进制浮点数
        /// Enum 类
        /// Int 系列
        /// Uint系列                
        /// Guid 结构  表示全局唯一标识符 (GUID)。
        /// HashCode 结构  将多个值的哈希代码合并为一个哈希代码        
        /// ValueTuple  提供用于创建值元组的静态方法  
        /// </summary>
        public static void CommonValueType()
        {

        }

        /// <summary>
        /// Datetime   表示某个时刻，通常以日期和当天的时间表示
        /// DateTimeOffset  表示一个时间点，通常以相对于协调世界时 (UTC) 的日期和时间来表示。
        /// TimeSpan 结构  表示时间间隔。
        /// DateOnly   表示从 0001 年 1 月 1 日 Anno Domini（共同时代）到 9999 年 12 月 31 日（C.E.）在公历中的值。
        /// TimeOnly 结构  表示在 00：00：00 到 23：59：59.999999 范围内从时钟读取的时间。
        /// </summary>
        public static void DateTimeOffsetLearn()
        {

            // DateTimeOffset 比 DateTime 多了时区的属性
            // TimeSpan 表示时间间隔。
            DateTime date1, date2;
            DateTimeOffset dateOffset1, dateOffset2;
            TimeSpan difference;

            // Find difference between Date.Now and Date.UtcNow
            date1 = DateTime.Now;
            date2 = DateTime.UtcNow;
            difference = date1 - date2;
            Console.WriteLine("{0} - {1} = {2}", date1, date2, difference);

            // Find difference between Now and UtcNow using DateTimeOffset
            dateOffset1 = DateTimeOffset.Now;
            dateOffset2 = DateTimeOffset.UtcNow;
            difference = dateOffset1 - dateOffset2;
            Console.WriteLine("{0} - {1} = {2}",
                              dateOffset1, dateOffset2, difference);

            //https://learn.microsoft.com/zh-cn/dotnet/standard/datetime/how-to-use-dateonly-timeonly

            //DateOnly 和 TimeOnly 结构是随 .NET 6 引入的，分别表示一个特定的日期或时间

            //在.NET 6 之前，以及在.NET Framework 中，开发人员使用 DateTime 类型（或某个其他替代项）来表示以下内容之一：

            //一个完整的日期和时间。
            //一个日期，不考虑时间。
            //一个时间，不考虑日期。
            //DateOnly 和 TimeOnly 是表示 DateTime 类型的这些特定部分的类型

            //DateOnly优点：
            //1.如果 DateTime 结构被时区偏移，它可能会滚动到上一天或第二天。 DateOnly 不能被时区偏移，并且始终表示所设置的日期。
            //2.序列化 DateTime 结构包括时间部分，这可能会掩盖数据的意图。 此外，DateOnly 序列化的数据更少。
            //3.当代码与数据库（如 SQL Server）交互时，整个日期通常存储为 date 数据类型，其中不包含时间。 DateOnly 与数据库类型更匹配。

            //将 DateTime 转换为 DateOnly
            {
                var today = DateOnly.FromDateTime(DateTime.Now);
                Console.WriteLine($"Today is {today}");
            }

            //加/减天数、月数、年数
            {
                var theDate = new DateOnly(2015, 10, 21);

                var nextDay = theDate.AddDays(1);
                var previousDay = theDate.AddDays(-1);
                var decadeLater = theDate.AddYears(10);
                var lastMonth = theDate.AddMonths(-1);

                Console.WriteLine($"Date: {theDate}");
                Console.WriteLine($" Next day: {nextDay}");
                Console.WriteLine($" Previous day: {previousDay}");
                Console.WriteLine($" Decade later: {decadeLater}");
                Console.WriteLine($" Last month: {lastMonth}");
            }


            //分析 DateOnly 并设置其格式
            {
                var theDate = DateOnly.ParseExact("21 Oct 2015", "dd MMM yyyy", CultureInfo.InvariantCulture);  // Custom format
                var theDate2 = DateOnly.Parse("October 21, 2015", CultureInfo.InvariantCulture);

                Console.WriteLine(theDate.ToString("m", CultureInfo.InvariantCulture));     // Month day pattern
                Console.WriteLine(theDate2.ToString("o", CultureInfo.InvariantCulture));    // ISO 8601 format
                Console.WriteLine(theDate2.ToLongDateString());
            }


            //比较 DateOnly
            {
                var theDate = DateOnly.ParseExact("21 Oct 2015", "dd MMM yyyy", CultureInfo.InvariantCulture);  // Custom format
                var theDate2 = DateOnly.Parse("October 21, 2015", CultureInfo.InvariantCulture);
                var dateLater = theDate.AddMonths(6);
                var dateBefore = theDate.AddDays(-10);

                Console.WriteLine($"Consider {theDate}...");
                Console.WriteLine($" Is '{nameof(theDate2)}' equal? {theDate == theDate2}");
                Console.WriteLine($" Is {dateLater} after? {dateLater > theDate} ");
                Console.WriteLine($" Is {dateLater} before? {dateLater < theDate} ");
                Console.WriteLine($" Is {dateBefore} after? {dateBefore > theDate} ");
                Console.WriteLine($" Is {dateBefore} before? {dateBefore < theDate} ");

            }

            //在引入 TimeOnly 类型之前，程序员通常使用 DateTime 类型或 TimeSpan 类型来表示特定时间。
            //但是，使用这些结构来模拟没有日期的时间可能会导致一些问题，而 TimeOnly 可以解决这些问题：

            //1.TimeSpan 表示经过的时间，例如使用秒表测量的时间。 上限范围超过 29,000 年，它的值可以是负值，表示在时间上向后移动。
            //负的 TimeSpan 并不表示一天中的特定时间。

            //2.如果将 TimeSpan 用作一天中的某个时间，则存在可能将其操作为 24 小时以外的值的风险。
            //TimeOnly 就没有这种风险。 例如，如果一个员工的工作班次从 18:00 开始且持续 8 小时，那么，向 TimeOnly 结构加上 8 小时将滚动到 2:00

            //3.要将 DateTime 用于一天中的某个时间，需要将任意日期与该时间相关联，然后被忽略。
            //通常的做法是选择 DateTime.MinValue(0001 - 01 - 01) 作为日期，但如果从 DateTime 值中减去小时数，可能会发生 OutOfRange 异常。
            //TimeOnly 就没有这个问题，因为时间在 24 小时的时间范围内前后滚动。

            //4.序列化 DateTime 结构包括日期部分，这可能会掩盖数据的意图。 此外，TimeOnly 序列化的数据更少。


            //将 DateTime 转换为 TimeOnly
            {
                var now = TimeOnly.FromDateTime(DateTime.Now);
                Console.WriteLine($"It is {now} right now");
            }

            //加/减时间
            {
                var theTime = new TimeOnly(7, 23, 11);
                var hourLater = theTime.AddHours(1);
                var minutesBefore = theTime.AddMinutes(-12);
                var secondsAfter = theTime.Add(TimeSpan.FromSeconds(10));
                var daysLater = theTime.Add(new TimeSpan(hours: 21, minutes: 200, seconds: 83), out int wrappedDays);
                var daysBehind = theTime.AddHours(-222, out int wrappedDaysFromHours);

                Console.WriteLine($"Time: {theTime}");
                Console.WriteLine($" Hours later: {hourLater}");
                Console.WriteLine($" Minutes before: {minutesBefore}");
                Console.WriteLine($" Seconds after: {secondsAfter}");
                Console.WriteLine($" {daysLater} is the time, which is {wrappedDays} days later");
                Console.WriteLine($" {daysBehind} is the time, which is {wrappedDaysFromHours} days prior");
            }

            //分析 TimeOnly 并设置其格式
            {
                var theTime = TimeOnly.ParseExact("5:00 pm", "h:mm tt", CultureInfo.InvariantCulture);  // Custom format
                var theTime2 = TimeOnly.Parse("17:30:25", CultureInfo.InvariantCulture);
                Console.WriteLine(theTime.ToString("o", CultureInfo.InvariantCulture));     // Round-trip pattern.
                Console.WriteLine(theTime2.ToString("t", CultureInfo.InvariantCulture));    // Long time format
                Console.WriteLine(theTime2.ToLongTimeString());
            }

            {
                //将 DateOnly 和 TimeOnly 类型序列化
                //在.NET 7 + 中，System.Text.Json 支持将 DateOnly 和 TimeOnly 类型序列化和反序列化。 请考虑以下对象

                AppointmentCls originalAppointment = new AppointmentCls(
     Guid.NewGuid(),
     "Take dog to veterinarian.",
     new DateOnly(2002, 1, 13),
     new TimeOnly(5, 15),
     new TimeOnly(5, 45));

                string serialized = JsonSerializer.Serialize(originalAppointment);

                Console.WriteLine($"Resulting JSON: {serialized}");

                AppointmentCls deserializedAppointment =
                    JsonSerializer.Deserialize<AppointmentCls>(serialized)!;

                bool valuesAreTheSame = originalAppointment == deserializedAppointment;
                Console.WriteLine($"""
    Original record has the same values as the deserialized record: {valuesAreTheSame}
    """);
            }

            //使用 TimeSpan 和 DateTime
            {

                {
                    // TimeSpan must in the range of 00:00:00.0000000 to 23:59:59.9999999
                    var theTime = TimeOnly.FromTimeSpan(new TimeSpan(23, 59, 59));
                    var theTimeSpan = theTime.ToTimeSpan();
                    Console.WriteLine($"Variable '{nameof(theTime)}' is {theTime}");
                    Console.WriteLine($"Variable '{nameof(theTimeSpan)}' is {theTimeSpan}");
                }

                {
                    var theTime = new TimeOnly(11, 25, 46);   // 11:25 AM and 46 seconds
                    var theDate = new DateOnly(2015, 10, 21); // October 21, 2015
                    var theDateTime = theDate.ToDateTime(theTime);
                    var reverseTime = TimeOnly.FromDateTime(theDateTime);
                    Console.WriteLine($"Date only is {theDate}");
                    Console.WriteLine($"Time only is {theTime}");
                    Console.WriteLine();
                    Console.WriteLine($"Combined to a DateTime type, the value is {theDateTime}");
                    Console.WriteLine($"Converted back from DateTime, the time is {reverseTime}");
                }
            }

            //算术运算符和比较 TimeOnly
            {
                var start = new TimeOnly(10, 12, 01); // 10:12:01 AM
                var end = new TimeOnly(14, 00, 53); // 02:00:53 PM

                var outside = start.AddMinutes(-3);
                var inside = start.AddMinutes(120);

                Console.WriteLine($"Time starts at {start} and ends at {end}");
                Console.WriteLine($" Is {outside} between the start and end? {outside.IsBetween(start, end)}");
                Console.WriteLine($" Is {inside} between the start and end? {inside.IsBetween(start, end)}");
                Console.WriteLine($" Is {start} less than {end}? {start < end}");
                Console.WriteLine($" Is {start} greater than {end}? {start > end}");
                Console.WriteLine($" Does {start} equal {end}? {start == end}");
                Console.WriteLine($" The time between {start} and {end} is {end - start}");
            }

        }


        /// <summary>
        /// Span<T> 结构,是 C# 7.2 中引入的一种结构体，用于表示连续的内存区域，提供了一种高效且安全的方式来读取和写入内存。
        /// 它可以直接操作数组、堆栈和堆等内存区域，避免不必要的内存分配和复制，从而提高代码性能和效率‌。
        /// 
        /// ReadOnlySpan<T> 结构  提供任意内存连续区域的类型安全且内存安全的只读表示形式。      
        /// </summary>
        public static void SpanLearn()
        {

            //使用场景和示例代码
            //Span<T>通常用于以下场景：
            ////‌数组操作‌：可以直接访问和修改数组元素，而不需要复制数据。
            //‌//性能优化‌：在需要高性能处理大量数据时，使用Span<T> 可以减少内存拷贝，提高效率。
            {
                //创建和使用‌：
                string[] arrStr = new string[] { "apple", "banana", "pear", "pineapple", "watermelon", "strawberry" };

                Span<string> strings0 = arrStr.AsSpan();
                Console.WriteLine(strings0[3]);
                Span<string> strings = new Span<string>(arrStr);
                Span<string> stringsSplit = new Span<string>(arrStr, 2, 4);

                //1-直接修改元素
                {
                    int[] numbers = { 1, 2, 3, 4, 5 };
                    Span<int> span = numbers;
                    span[0] = 10; // 修改数组第一个元素的值为10
                    Console.WriteLine(numbers[0]); // 输出10
                }

                //使用Span<T>查找数组中的最大值，这种方法直接操作内存，减少了数据复制的开销。
                {
                    int[] numbers = GenerateRandomArray(1000000);
                    Span<int> span = new Span<int>(numbers);
                    int max = FindMaxValue(span);
                    Console.WriteLine($"最大值: {max}");

                    static int[] GenerateRandomArray(int length)
                    {
                        Random random = new Random();
                        int[] array = new int[length];
                        for (int i = 0; i < length; i++)
                        {
                            array[i] = random.Next(1000); // 生成随机整数
                        }
                        return array;
                    }

                    static int FindMaxValue(Span<int> span)
                    {
                        int max = span[0];
                        for (int i = 1; i < span.Length; i++)
                        {
                            if (span[i] > max)
                            {
                                max = span[i];
                            }
                        }
                        return max;
                    }
                }

                //创建切片
                //Span<T> 的一个强大特性是，可以使用它访问数组的部分或切片。使用切片时，不会复制数组元素，它们是从span 中直接访问的。
                {
                    int[] source = { 1, 6, 23, 76, 88, 213 };
                    Span<int> span1 = new Span<int>(source, start: 1, length: 4);
                    Span<int> span2 = span1.Slice(start: 1, length: 3);

                    DisplaySpan("span1 contains the elements:", span1);
                    DisplaySpan("span2 contains the elements:", span2);

                    static void DisplaySpan(string content, Span<int> span1)
                    {
                        Console.Write(content);
                        foreach (var item in span1)
                        {
                            Console.Write(item + ",");
                        }
                        Console.WriteLine();
                    }
                }
                {
                    //使用Span改变值
                    //在文章开头，介绍了如何使用 Span<T> 的索引器，直接更改由 span 直接引用的数组元素，实际上它还有其他改变值的方法。

                    //例如：
                    //Slice(int start, int length)：返回一个新的 Span< T >，它表示从 Span<T> 的指定起始位置开始的指定长度部分。可以使用该方法来获取或更改 Span< T > 中的子集。
                    //Clear()：将 Span<T> 中的所有元素设置为默认值 default < T >。
                    //Fill(T value)：将 Span<T> 中的所有元素设置为指定的值。
                    //CopyTo(Span<T> destination)：将 Span< T > 中的所有元素复制到指定的目标 Span < T >。
                    //CopyTo(T[] destination)：将 Span<T> 中的所有元素复制到指定的目标数组。
                    //Reverse()：反转 Span< T > 中的元素顺序。
                    //Sort()：对 Span<T> 中的元素进行排序。
                }
                //Span<T>可以用于高效地处理字符串，例如字符串拆分、搜索、替换等操作，避免不必要的字符串分配和拷贝。
                {
                    string text = "Hello, World!";
                    var span = text.AsSpan();
                    // 假设我们要反转字符串
                    char[] reversed = new char[span.Length];
                    for (int i = 0; i < span.Length; i++)
                    {
                        reversed[i] = span[span.Length - 1 - i];
                    }
                    string reversedText = new string(reversed);
                    Console.WriteLine(reversedText); // 输出 "!dlroW ,olleH"
                }

                //优化网络数据处理
                {
                    byte[] data = new byte[1024]; // 假设这是从网络接收的数据
                    Span<byte> span = new Span<byte>(data);
                    // 使用Span<T>处理数据，例如解析协议
                    // ...

                }

                //适用场景：
                //数组操作:  使用 Span<T> 查找数组中的最大值，可以直接修改数组中的元素,减少数据复制的开销
                //字符串操作:Span<T> 可以用于高效的字符串处理，如拆分、搜索和替换，避免不必要的字符串分配和复制
                //内存池管理:可以与内存池配合使用，提高内存分配和释放效率，减轻垃圾回收（GC）压力‌
                //文件I/O操作:在文件读写操作中，使用 Span<T> 可以减少内存复制开销，提高读写效率
                //网络编程:在网络编程中，Span<T> 可以处理网络数据包，解析协议，提高数据处理效率‌
                //异步编程 ?

                SpanBenchmark spanBenchmark = new SpanBenchmark();
                //spanBenchmark.TestSplitStringWithSpan();

                //spanBenchmark.TestFilterWithSpan();

                //限制和注意事项
                ////‌生命周期‌：Span<T> 是值类型，它不能是非堆栈类型的字段，不能被装箱，也不能是异步方法的参数或局部变量‌。
                ////‌只读版本‌：ReadOnlySpan<T> 是 Span<T> 的只读版本，通常用于字符串的切片操作，因为字符串是不可变的‌。
           
            }
        }

        /// <summary>
        /// Memory<T> 连续内存区域的包装器。 Memory<T> 实例可以由 T 类型数组、String 或内存管理器提供支持。 
        /// 因为 Memory<T> 可以存储在托管堆上，所以它没有任何 Span<T> 限制。
        /// ReadOnlyMemory<T>  表示连续的内存区域，类似于ReadOnlySpan<T>。与ReadOnlySpan<T>不同，它不是一个类似byref的类型。
        /// </summary>
        public static void MemoryLearn()
        {
            //类似 Span<T>， Memory<T> 表示内存的连续区域。 但是Memory<T>，与引用结构不同Span<T>。 这意味着 Memory< T > 可以放置在托管堆上，而 Span<T> 不能。 因此，结构 Memory< T > 没有与 Span<T> 实例相同的限制。 具体而言：

            //它可以用作类中的字段。

            //它可以跨await``yield边界使用。

            //此外 Memory<T>，还可以用于 System.ReadOnlyMemory<T> 表示不可变内存或只读内存
        }


        /// <summary>
        /// DictionaryEntry 结构  定义可设置或检索的字典键/值对。
        /// Index 结构
        /// IntPtr  
        /// Range   表示具有开始和结束索引的范围。
        /// </summary>
        public static void OtherLearn()
        {
        }
    }

    public class AppointmentCls
    {
        public AppointmentCls(Guid id, string description, DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            Id = id;
            Description = description;
            Date = date;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
