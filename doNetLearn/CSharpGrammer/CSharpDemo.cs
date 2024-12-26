using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.CSharpGrammer
{
    internal class CSharpDemo
    {
        /// <summary>
        /// 自动获得的功能:
        /// </summary>
        class RecordDemo
        {
            /// <summary>
            /// 1.不可变性
            /// </summary>
            public void Method1()
            {
                var person = new PersonRecord("John", "Doe");

               // person.FirstName = "Jane";  // 这行代码无法编译

                // 相反，你需要创建一个带有更改的新记录：
                var updatedPerson = person with { FirstName = "Jane" };
            }

            public void Method2()
            {

                var person1 = new PersonClass("John", "Doe");
                var person2 = new PersonClass("John", "Doe");
                Console.WriteLine(person1 == person2); // False！不同的引用

                var record1 = new PersonRecord("John", "Doe");
                var record2 = new PersonRecord("John", "Doe");
                Console.WriteLine(record1 == record2); // True！相同的数据 = 相等

            }
            /// <summary>
            /// 轻松复制并修改
            /// </summary>
            public void Method3()
            {
                var original = new PersonRecord("John", "Doe");

                // 创建一个只改变 FirstName 的新记录：
                var updated = original with { FirstName = "Jane" };
            }


            //为什么 Records 会（稍微）慢一些
            //PerformanceComparison


            /*
             * 
            开销来自于：
                生成的相等性方法
                基于值的比较代码
                额外的安全检查
             * 
             */

            //为什么要不顾开销也要使用 Records
            ////1.开发者生产力

            ////对于 API 响应，如果我们使用类，则需要大量代码：
            public class ApiResponseClass<T>
            {
                public T Data { get; init; }
                public bool Success { get; init; }
                public string? Message { get; init; }
                public DateTime Timestamp { get; init; }

                // 需要构造函数
                // 需要相等性比较
                // 需要 ToString
                // 需要哈希码
                // 太多样板代码！
            }
            //使用 record — 一行搞定！
            public record ApiResponseRecord<T>(T Data, bool Success, string? Message, DateTime Timestamp);

            ////2.不可变性 = 线程安全
            ////因为 records 是不可变的，所以这是线程安全的：
            ///
            public record Configuration(
    string ApiKey,
    string BaseUrl,
    int Timeout
);

            // 可以安全地在线程间共享
            public class Service
            {
                private readonly Configuration _config;

                public Service(Configuration config)
                {
                    _config = config;
                }

                // 不需要锁 - 配置无法更改！
            }


            ////3.非常适合领域事件
            ///


            //records该做与不该做
            ////不要 1. 深层 Record 层次结构可能会很慢  每次相等性检查都必须遍历整个层次结构！
            //public record Entity(Guid Id);
            //public record Person(Guid Id, string Name) : Entity(Id);
            //public record Employee(Guid Id, string Name, decimal Salary) : Person(Id, Name);
            //public record Manager(Guid Id, string Name, decimal Salary, string Department)
            //    : Employee(Id, Name, Salary);

            //而是要 1.使用组合
            //public record Manager(
            //    Guid Id,
            //    PersonInfo Person,
            //    EmployeeInfo Employment,
            //    string Department
            //);


            ////2.使用集合时要小心
            //这每次都会创建一个新列表
            public record UserList(List<User> Users)
            {
                public UserList AddUser(User user) =>
                    this with { Users = new List<User>(Users) { user } };
            }

            //更好的方式
            public class UserCollection
            {
                private readonly List<User> _users = new();
                public IReadOnlyList<User> Users => _users.AsReadOnly();

                public void AddUser(User user) => _users.Add(user);
            }
        }

        /// <summary>
        /// 使用新的 record 方式 — PersonRecord实现与PersonClass完全相同的功能
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        public record PersonRecord(string FirstName, string LastName);

        public class PersonClass
        {
            public string FirstName { get; init; }
            public string LastName { get; init; }

            public PersonClass(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            // 需要实现相等性比较
            public override bool Equals(object? obj)
            {
                if (obj is not PersonClass other) return false;
                return FirstName == other.FirstName &&
                       LastName == other.LastName;
            }

            // 集合需要这个
            public override int GetHashCode()
            {
                return HashCode.Combine(FirstName, LastName);
            }

            // 调试需要这个
            public override string ToString()
            {
                return $"Person {{ FirstName = {FirstName}, LastName = {LastName} }}";
            }



        }

        // 基准测试：创建100万个实例
        public class PerformanceComparison
        {
            private const int Iterations = 1_000_000;

            [Benchmark]
            public void CreateClasses()
            {
                for (int i = 0; i < Iterations; i++)
                {
                    var person = new PersonClass("John", "Doe");
                }
            }

            [Benchmark]
            public void CreateRecords()
            {
                for (int i = 0; i < Iterations; i++)
                {
                    var person = new PersonRecord("John", "Doe");
                }
            }
        }



        //总结：
        /*
         * 
        | **适合使用 Records 的场景**  | **避免使用 Records 的场景** |
        |------------------------------|------------------------|
        | DTOs 和 API 契约             | 需要频繁更新的对象           |
        | 配置对象                     | 深层继承层次结构             |
        | 领域事件                     | 大型可变集合               |
        | 值对象                       | 复杂业务逻辑               |
        | 任何不可变数据结构            |                        |
         * 
         */
    }
}
