using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.CSharpGrammer
{
    /// <summary>
    /// init 关键字在属性或索引器中定义访问器方法。 init-only 资源库仅在对象构造期间为属性或索引器元素赋值。 
    /// init 强制实施不可变性，因此，一旦初始化对象，将无法更改。 init 访问器使调用代码能够使用对象初始值设定项来设置初始值。 
    /// 相比之下，只有一个 get 资源库的自动实现的属性必须通过调用构造函数进行初始化。 
    /// 具有 private set 访问器的属性可在构造后修改，但只能在类中修改。
    /// </summary>
    internal class initDemo
    {
        class Person_InitExampleNonNull
        {
            private int _yearOfBirth;

            public required int YearOfBirth   //若要强制调用方设置初始非 null 值，可添加 required 修饰符
            {
                get => _yearOfBirth;
                init => _yearOfBirth = value;
            }
        }

        class PersonPrivateSet
        {
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public PersonPrivateSet(string first, string last) => (FirstName, LastName) = (first, last);

            public void ChangeName(string first, string last) => (FirstName, LastName) = (first, last);
        }

        class PersonReadOnly
        {
            public string FirstName { get; }
            public string LastName { get; }
            public PersonReadOnly(string first, string last) => (FirstName, LastName) = (first, last);
        }

        class PersonInit
        {
            public string FirstName { get; init; }
            public string LastName { get; init; }
        }

        /// <summary>
        /// 示例显示了 private set、read only 和 init 属性之间的区别
        /// </summary>
        public static void Method()
        {
            //private set 版本和 read only 版本都需要调用方使用添加的构造函数来设置 name 属性。 
            PersonPrivateSet personPrivateSet = new("Bill", "Gates");
            //通过 private set 版本，人员可在构造实例后更改其名称。
            personPrivateSet.ChangeName("Amy", "Wall");

            PersonReadOnly personReadOnly = new("Bill", "Gates");

            // init 版本不需要构造函数,调用方可使用对象初始值设定项初始化属性
            PersonInit personInit = new() { FirstName = "Bill", LastName = "Gates" };
            //personInit.FirstName = "wrew";
            Console.WriteLine(personPrivateSet);

        }
    }
}
