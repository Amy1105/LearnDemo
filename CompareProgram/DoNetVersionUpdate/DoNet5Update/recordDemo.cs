using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DoNetVersionUpdate.DoNet5Update
{
    /// <summary>
    /// 虽然记录可以是可变的，但它们主要用于支持不可变的数据模型
    /// </summary>
    public class recordDemo
    {
        //记录record的特点：可以修饰值类型，也可以修饰引用类型

        //可以使用System.Text.Json.Serialization.JsonPropertyNameAttribute。修饰生成json的属性
        //property: 目标指示该特性应用于编译器生成的属性。 其他值包括将该特性应用于字段的 field:，以及将该特性应用于参数的 param:。

        #region 在创建实例时，可以使用位置参数来声明记录的属性，并初始化属性值
        public record Person(string FirstName, string LastName);

        public static void Method()
        {
            Person person = new("Nancy", "Davolio");
            Console.WriteLine(person);
            // output: Person { FirstName = Nancy, LastName = Davolio }
        }


        public record PersonForJson([property: JsonPropertyName("firstName")] string FirstName,
    [property: JsonPropertyName("lastName")] string LastName);



        /// <summary>
        /// 如果生成的自动实现的属性定义并不是你所需要的，你可以自行定义同名的属性
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="Id"></param>
        public record Person3(string FirstName, string LastName, string Id)
        {
            internal string Id { get; init; } = Id;
        }

        public static void Method3()
        {
            Person3 person = new("Nancy", "Davolio", "12345");
            Console.WriteLine(person.FirstName); //output: Nancy

        }


        /// <summary>
        /// 记录类型不需要声明任何位置属性。 你可以在没有任何位置属性的情况下声明一个记录，也可以声明其他字段和属性
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        public record Person4(string FirstName, string LastName)
        {
            public string[] PhoneNumbers { get; init; } = [];
        };


        #region 不可变性--展示了引用型不可变属性的内容（本例中是数组）是可变的

        public record Person5(string FirstName, string LastName, string[] PhoneNumbers);

        public static void Method5()
        {
            Person5 person = new("Nancy", "Davolio", new string[1] { "555-1234" });
            Console.WriteLine(person.PhoneNumbers[0]); // output: 555-1234

            person.PhoneNumbers[0] = "555-6789";
            Console.WriteLine(person.PhoneNumbers[0]); // output: 555-6789
        }

        #endregion



        #region  值相等性

        /*
         如果不替代或替换相等性方法，则声明的类型将控制如何定义相等性：
            对于 class 类型，如果两个对象引用内存中的同一对象，则这两个对象相等。
            对于 struct 类型，如果两个对象是相同的类型并且存储相同的值，则这两个对象相等。
            对于具有 record 修饰符（record class、record struct 和 readonly record struct）的类型，如果两个对象是相同的类型并且存储相同的值，则这两个对象相等
         */

        /// <summary>
        /// record struct 的相等性定义与 struct 的相等性定义相同。 不同之处在于，对于 struct，实现处于 ValueType.Equals(Object)
        /// 中并且依赖反射。 对于记录，实现由编译器合成，并且使用声明的数据成员。
        /// </summary>
        public static void Method6()
        {
            var phoneNumbers = new string[2];
            Person5 person1 = new("Nancy", "Davolio", phoneNumbers);
            Person5 person2 = new("Nancy", "Davolio", phoneNumbers);
            Console.WriteLine(person1 == person2); // output: True

            person1.PhoneNumbers[0] = "555-1234";
            Console.WriteLine(person1 == person2); // output: True
            Console.WriteLine(string.Join(',', person1.PhoneNumbers));
            Console.WriteLine(string.Join(',', person2.PhoneNumbers));
            Console.WriteLine(ReferenceEquals(person1, person2)); // output: False
        }

        #endregion


        #region 非破坏性变化
        //with 表达式创建一个新的记录实例，该实例是现有记录实例的一个副本，修改了指定属性和字段
        public record Person7(string FirstName, string LastName)
        {
            public string[] PhoneNumbers { get; init; }
        }

        /// <summary>
        /// 如果需要复制包含一些修改的实例，可以使用 with 表达式来实现非破坏性变化
        /// </summary>
        public static void Method7()
        {
            Person7 person1 = new("Nancy", "Davolio") { PhoneNumbers = new string[1] };
            Console.WriteLine(person1);
            // output: Person { FirstName = Nancy, LastName = Davolio, PhoneNumbers = System.String[] }

            Person7 person2 = person1 with { FirstName = "John" };
            Console.WriteLine(person2);
            // output: Person { FirstName = John, LastName = Davolio, PhoneNumbers = System.String[] }
            Console.WriteLine(person1 == person2); // output: False

            person2 = person1 with { PhoneNumbers = new string[1] };
            Console.WriteLine(person2);
            // output: Person { FirstName = Nancy, LastName = Davolio, PhoneNumbers = System.String[] }
            Console.WriteLine(person1 == person2); // output: False

            person2 = person1 with { };
            Console.WriteLine(person1 == person2); // output: True
        }
        #endregion

        #region 用于显示的内置格式设置
        //记录类型具有编译器生成的 ToString 方法，可显示公共属性和字段的名称和值
        public record Person8(string FirstName, string LastName) 
        {
            public override string ToString()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("Teacher"); // type name
                stringBuilder.Append(" { ");
                if (PrintMembers(stringBuilder))
                {
                    stringBuilder.Append(" ");
                }
                stringBuilder.Append("}");
                return stringBuilder.ToString();
            }
        }
        
        public static void Method8()
        {
          var p=new  Person8 ("Nancy", "Davolio");
            Console.WriteLine(p.ToString());
        }

        #endregion

        #region  继承 仅适用于record class

        //一条记录可以从另一条记录继承。 但是，记录不能从类继承，类也不能从记录继承

        public abstract record Person9(string FirstName, string LastName);
        public record Teacher(string FirstName, string LastName, int Grade): Person9(FirstName, LastName);
        public static void Method9()
        {
            Person9 teacher = new Teacher("Nancy", "Davolio", 3);
            Console.WriteLine(teacher);
            // output: Teacher { FirstName = Nancy, LastName = Davolio, Grade = 3 }
        }
        //继承层次结构中的相等性
       
        public record Student(string FirstName, string LastName, int Grade)
            : Person9(FirstName, LastName);
        public static void Method10()
        {
            Person9 teacher = new Teacher("Nancy", "Davolio", 3);
            Person9 student = new Student("Nancy", "Davolio", 3);
            Console.WriteLine(teacher == student); // output: False
            Student student2 = new Student("Nancy", "Davolio", 3);
            Console.WriteLine(student2 == student); // output: True
        }


        #endregion


        #region 派生记录中的 with 表达式

        public record Point(int X, int Y)
        {
            public int Zbase { get; set; }
        };
        public record NamedPoint(string Name, int X, int Y) : Point(X, Y)
        {
            public int Zderived { get; set; }
        };

        /// <summary>
        /// with 表达式结果的运行时间类型与表达式操作数相同： 运行时类型的所有属性都会被复制，但你只能设置编译时类型的属性
        /// </summary>
        public static void Method11()
        {
            Point p1 = new NamedPoint("A", 1, 2) { Zbase = 3, Zderived = 4 };

            Point p2 = p1 with { X = 5, Y = 6, Zbase = 7 }; // Can't set Name or Zderived
            Console.WriteLine(p2 is NamedPoint);  // output: True
            Console.WriteLine(p2);
            // output: NamedPoint { X = 5, Y = 6, Zbase = 7, Name = A, Zderived = 4 }

            Point p3 = (NamedPoint)p1 with { Name = "B", X = 5, Y = 6, Zbase = 7, Zderived = 8 };
            Console.WriteLine(p3);
            // output: NamedPoint { X = 5, Y = 6, Zbase = 7, Name = B, Zderived = 8 }
        }

        #endregion


        #region  



        public abstract record Person11(string FirstName, string LastName);
        public record Teacher11(string FirstName, string LastName, int Grade)
            : Person11(FirstName, LastName);
        public record Student11(string FirstName, string LastName, int Grade)
            : Person11(FirstName, LastName);

        public static void Method12()
        {
            Person11 teacher = new Teacher11("Nancy", "Davolio", 3);
            Console.WriteLine(teacher);
            // output: Teacher { FirstName = Nancy, LastName = Davolio, Grade = 3 }
        }


        public abstract record Person12(string FirstName, string LastName, string[] PhoneNumbers)
        {
            protected virtual bool PrintMembers(StringBuilder stringBuilder)
            {
                stringBuilder.Append($"FirstName = {FirstName}, LastName = {LastName}, ");
                stringBuilder.Append($"PhoneNumber1 = {PhoneNumbers[0]}, PhoneNumber2 = {PhoneNumbers[1]}");
                return true;
            }
        }

        public record Teacher12(string FirstName, string LastName, string[] PhoneNumbers, int Grade)
            : Person12(FirstName, LastName, PhoneNumbers)
        {
            protected override bool PrintMembers(StringBuilder stringBuilder)
            {
                if (base.PrintMembers(stringBuilder))
                {
                    stringBuilder.Append(", ");
                };
                stringBuilder.Append($"Grade = {Grade}");
                return true;
            }
        };

        public static void Method13()
        {
            Person12 teacher = new Teacher12("Nancy", "Davolio", new string[2] { "555-1234", "555-6789" }, 3);
            Console.WriteLine(teacher);
            // output: Teacher { FirstName = Nancy, LastName = Davolio, PhoneNumber1 = 555-1234, PhoneNumber2 = 555-6789, Grade = 3 }
        }

        #endregion


        #region  派生记录中的解构函数行为
        public abstract record Person13(string FirstName, string LastName);
        public record Teacher13(string FirstName, string LastName, int Grade)
            : Person13(FirstName, LastName);
        public record Student13(string FirstName, string LastName, int Grade)
            : Person13(FirstName, LastName);

        public static void Method14()
        {
            Person13 teacher = new Teacher13("Nancy", "Davolio", 3);
            var (firstName, lastName) = teacher; // Doesn't deconstruct Grade
            Console.WriteLine($"{firstName}, {lastName}");// output: Nancy, Davolio

            var (fName, lName, grade) = (Teacher13)teacher;
            Console.WriteLine($"{fName}, {lName}, {grade}");// output: Nancy, Davolio, 3
        }

        #endregion


        #endregion

        //仅限 Init 的资源库
        //顶级语句
        //模式匹配增强：关系模式和逻辑模式
        //性能和互操作性
        //本机大小的整数
        //函数指针
        //禁止发出 localsinit 标志
        //模块初始值设定项
        //分部方法的新功能
        //调整和完成功能
        //目标类型的 new 表达式
        //static 匿名函数
        //目标类型的条件表达式
        //协变返回类型
        //扩展 GetEnumerator 支持 foreach 循环
        //Lambda 弃元参数
        //本地函数的属性
    }
}
