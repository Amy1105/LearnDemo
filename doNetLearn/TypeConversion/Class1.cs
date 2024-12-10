using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace doNetLearn.TypeConversion
{
    /// <summary>
    /// .NET 中的类型转换  https://learn.microsoft.com/zh-cn/dotnet/standard/base-types/type-conversion?redirectedfrom=MSDN
    /// .NET 自动支持以下转换：
    /// 1、从派生类转换为基类。 例如，这意味着可将任何类或结构的实例转换为 Object 实例。 此转换不需要强制转换或转换运算符
    /// 2、从基类转换回原始的派生类。 在 C# 中，此转换需要强制转换运算符。 在 Visual Basic 中，如果 Option Strict 处于开启状态，则它需要 CType 运算符。
    /// 3、从实现接口的类型转换为表示该接口的接口对象。 此转换不需要强制转换或转换运算符。
    /// 4、从接口对象转换回实现该接口的原始类型。 在 C# 中，此转换需要强制转换运算符。 在 Visual Basic 中，如果 Option Strict 处于开启状态，则它需要 CType 运算符。
    /// 
    /// 除这些自动转换外，.NET 还提供支持自定义类型转换的多种功能。 其中包括：
    /// Implicit 运算符，该运算符定义类型之间可用的扩大转换。
    /// Explicit 运算符，该运算符定义类型之间可用的收缩转换。
    /// IConvertible 接口，该接口定义到.NET 每个基数据类型的转换。
    /// Convert 类，该类提供了一组方法来实现 IConvertible 接口中的方法。
    /// TypeConverter 类，该类是一个基类，可以扩展该类以支持指定的类型到任何其他类型的转换。
    /// </summary>
    internal class TypeConversionDemo
    {

        /// <summary>
        /// 1、从派生类转换为基类。 例如，这意味着可将任何类或结构的实例转换为 Object 实例。 此转换不需要强制转换或转换运算符
        /// </summary>
        public void Method()
        {
            
            Student student = new Student(1, "tom", "高三的学生", 17);
            SpecialStudent  specialStudent = new SpecialStudent(2, "danel", "高三的学生", 17,"special","specialComment");
          
            //IS
            //a.检查对象类型的兼容性，并返回结果：true或者false；　　
            //b.不会抛出异常；　　
            //c.如果对象为null，则返回值永远为false
            if (specialStudent is Student)
            {

            }

            //AS
            //a.检查对象类型的兼容性，并返回结果，如果不兼容就返回null；　　
            //b.不会抛出异常；　　
            //c.如果结果判断为空，则强制执行类型转换将抛出NullReferenceExcep异常；　　
            //d.as必须和引用类型一起使用。

            Student b = (Student)specialStudent;
            // 如果类型转换不成功，会抛出异常　　
            // 对于上面的as操作，等效于下面的is操作：　　　　XX b = o is XX ? (XX)o: null ;　　实现的语法更加简明，且不会引发异常，在类型转换时值得推荐。
           
            //综上所述，as模式较is模式执行效率上更胜一筹，但是通常来说，is用于进行类型判断，as用于类型转型

        }

        /// <summary>
        /// 2、从基类转换回原始的派生类。 在 C# 中，此转换需要强制转换运算符。 在 Visual Basic 中，如果 Option Strict 处于开启状态，则它需要 CType 运算符。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Student Method2()
        {
            Student student = new Student(1, "tom", "高三的学生", 17);
            SpecialStudent specialStudent = new SpecialStudent(2, "danel", "高三的学生", 17, "special", "specialComment");

            var student1 = specialStudent as Student;

            //基类转成派生类，失败了

            var special = student as SpecialStudent;  //使用as关键字进行安全转换：

            var stu = (SpecialStudent)student;   //使用直接类型转换

            return stu;
        }
      
        public void Method3()
        {
            //3、从实现接口的类型转换为表示该接口的接口对象。 此转换不需要强制转换或转换运算符。
            CommonInterface commonInterface = new CommonClass();


            //4、从接口对象转换回实现该接口的原始类型。 在 C# 中，此转换需要强制转换运算符。 在 Visual Basic 中，如果 Option Strict 处于开启状态，则它需要 CType 运算符。
            CommonClass commonClass = new CommonClass();
            CommonInterface commonInterface2= commonClass as CommonInterface;



        }


        public void Method4()
        {
            SByte sbyteValue = -120;
            ByteWithSign value = sbyteValue;
            Console.WriteLine(value);
            value = Byte.MaxValue;
            Console.WriteLine(value);
            // The example displays the following output:
            //       -120
            //       255
        }

        /// <summary>
        /// 收缩转换之所以需要使用转换方法或强制转换运算符，主要是为提醒开发人员可能会丢失数据或引发 OverflowException，
        /// 以便可以在代码中对其进行处理。 但是，有些编译器可以放宽此要求。
        /// </summary>
        public void Method5()
        {
            long number1 = int.MaxValue + 20L;
            uint number2 = int.MaxValue - 1000;
            ulong number3 = int.MaxValue;

            int intNumber;

            try
            {
                intNumber = checked((int)number1);
                Console.WriteLine("After assigning a {0} value, the Integer value is {1}.",
                                  number1.GetType().Name, intNumber);
            }
            catch (OverflowException)
            {
                if (number1 > int.MaxValue)
                    Console.WriteLine("Conversion failed: {0} exceeds {1}.",
                                      number1, int.MaxValue);
                else
                    Console.WriteLine("Conversion failed: {0} is less than {1}.",
                                      number1, int.MinValue);
            }

            try
            {
                intNumber = checked((int)number2);
                Console.WriteLine("After assigning a {0} value, the Integer value is {1}.",
                                  number2.GetType().Name, intNumber);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Conversion failed: {0} exceeds {1}.",
                                  number2, int.MaxValue);
            }

            try
            {
                intNumber = checked((int)number3);
                Console.WriteLine("After assigning a {0} value, the Integer value is {1}.",
                                  number3.GetType().Name, intNumber);
            }
            catch (OverflowException)
            {
                Console.WriteLine("Conversion failed: {0} exceeds {1}.",
                                  number1, int.MaxValue);
            }

            // The example displays the following output:
            //    Conversion failed: 2147483667 exceeds 2147483647.
            //    After assigning a UInt32 value, the Integer value is 2147482647.
            //    After assigning a UInt64 value, the Integer value is 2147483647.
        }

        public void Method6()
        {
            int largeValue = Int32.MaxValue;
            byte newValue;

            try
            {
                newValue = unchecked((byte)largeValue);
                Console.WriteLine("Converted the {0} value {1} to the {2} value {3}.",
                                  largeValue.GetType().Name, largeValue,
                                  newValue.GetType().Name, newValue);
            }
            catch (OverflowException)
            {
                Console.WriteLine("{0} is outside the range of the Byte data type.",
                                  largeValue);
            }

            try
            {
                newValue = checked((byte)largeValue);
                Console.WriteLine("Converted the {0} value {1} to the {2} value {3}.",
                                  largeValue.GetType().Name, largeValue,
                                  newValue.GetType().Name, newValue);
            }
            catch (OverflowException)
            {
                Console.WriteLine("{0} is outside the range of the Byte data type.",
                                  largeValue);
            }
            // The example displays the following output:
            //    Converted the Int32 value 2147483647 to the Byte value 255.
            //    2147483647 is outside the range of the Byte data type.
        }
   
    
        public void Method7()
        {
            ByteWithSignE value;

            try
            {
                int intValue = -120;
                value = (ByteWithSignE)intValue;
                Console.WriteLine(value);
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                uint uintValue = 1024;
                value = (ByteWithSignE)uintValue;
                Console.WriteLine(value);
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }
            // The example displays the following output:
            //       -120
            //       '1024' is out of range of the ByteWithSignE data type.
        }


        /// <summary>
        /// 为了支持任意类型到公共语言运行时基类型的转换，.NET 提供了 IConvertible 接口
        /// 
        /// 大多数情况下，IConvertible 接口设计为支持 .NET 中基类型之间的转换。
        /// 但是，通过自定义类型也可以实现该接口，以便支持该类型到其他自定义类型的转换。
        /// </summary>
        public void IConvertibleDemo()
        {
            int codePoint = 1067;
            IConvertible iConv = codePoint;
            char ch = iConv.ToChar(null);
            Console.WriteLine("Converted {0} to {1}.", codePoint, ch);
        }

        /// <summary>
        /// 虽然可以调用每个基类型的 IConvertible 接口实现来执行类型转换，但从一种基类型转换为另一种基类型时，建议您调用 System.Convert 类的方法，
        /// 这种方式与语言无关。 此外，Convert.ChangeType(Object, Type, IFormatProvider) 方法还可用于从一个指定的自定义类型转换为另一种类型。
        /// </summary>
        public void Method8()
        {
            //基类型之间的转换



            //使用 ChangeType 方法的自定义转换

        }
    }

    public struct ByteWithSign
    {
        private SByte signValue;
        private Byte value;

        public static implicit operator ByteWithSign(SByte value)
        {
            ByteWithSign newValue;
            newValue.signValue = (SByte)Math.Sign(value);
            newValue.value = (byte)Math.Abs(value);
            return newValue;
        }

        public static implicit operator ByteWithSign(Byte value)
        {
            ByteWithSign newValue;
            newValue.signValue = 1;
            newValue.value = value;
            return newValue;
        }

        public override string ToString()
        {
            return (signValue * value).ToString();
        }
    }


    public struct ByteWithSignE
    {
        private SByte signValue;
        private Byte value;

        private const byte MaxValue = byte.MaxValue;
        private const int MinValue = -1 * byte.MaxValue;

        public static explicit operator ByteWithSignE(int value)
        {
            // Check for overflow.
            if (value > ByteWithSignE.MaxValue || value < ByteWithSignE.MinValue)
                throw new OverflowException(String.Format("'{0}' is out of range of the ByteWithSignE data type.",
                                                          value));

            ByteWithSignE newValue;
            newValue.signValue = (SByte)Math.Sign(value);
            newValue.value = (byte)Math.Abs(value);
            return newValue;
        }

        public static explicit operator ByteWithSignE(uint value)
        {
            if (value > ByteWithSignE.MaxValue)
                throw new OverflowException(String.Format("'{0}' is out of range of the ByteWithSignE data type.",
                                                          value));

            ByteWithSignE newValue;
            newValue.signValue = 1;
            newValue.value = (byte)value;
            return newValue;
        }

        public override string ToString()
        {
            return (signValue * value).ToString();
        }
    }

    public class Student
    {      
        public Student(int id, string name, string description, int age)
        {
            Id = id;
            Name = name;
            Description = description;
            Age = age;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; } = 0;

        /// <summary>
        /// 生日
        /// </summary>
        public DateOnly Birth { get; set; }

        /// <summary>
        /// 考试开始时间
        /// </summary>
        public TimeOnly Time { get; set; }
    }


    public class SpecialStudent : Student
    {      
        public SpecialStudent(int id, string name, string description, int age,string specialType, string comment):base(id, name, description, age)
        {
            SpecialType = specialType;
            Comment = comment;
        }

        public string SpecialType { get; set; }

        public string Comment {  get; set; }
    }


    public interface CommonInterface
    {
        int Id { get; set; }

        public string GetString();
    }

    public class CommonClass : CommonInterface
    {      
       public int Id { get =>1; set => throw new NotImplementedException(); }

        public string GetString()
        {
            return "CommonClass-string";
        }
    }
}
