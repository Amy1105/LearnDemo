using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.CSharpGrammer
{
    /// <summary>
    /// Convert解析
    /// </summary>
    internal class ConvertsMethod
    {
        /// <summary>
        /// 类型转换-ChangeType
        /// ChangeType 是一种通用转换方法，用于将 value 指定的对象转换为 conversionType。
        /// value 参数可以是任何类型的对象，conversionType 也可以是表示任何基类型或自定义类型的 Type 对象。 
        /// 若要成功转换，value 必须实现 IConvertible 接口，因为该方法只是包装对适当 IConvertible 方法的调用。
        /// 该方法要求支持将 value 转换为 conversionType。
        /// 此方法使用当前线程的区域性进行转换。
        /// </summary>
        public void ChangeTypeMethod()
        {
            Console.WriteLine("返回指定类型的对象，其值等于指定对象");

            Double d1 = -2.345;
            int i = (int)Convert.ChangeType(d1, typeof(int));

            Console.WriteLine("The double value {0} when converted to an int becomes {1}", d, i);

            string s = "12/12/98";
            DateTime dt = (DateTime)Convert.ChangeType(s, typeof(DateTime));

            Console.WriteLine("The string value {0} when converted to a Date becomes {1}", s, dt);

            Continent cont = Continent.NorthAmerica;
            Console.WriteLine("{0:N2}", Convert.ChangeType(cont, typeof(Double)));
            Console.WriteLine("{0:N2}", Convert.ChangeType(cont, typeof(String)));
            double number = 6;
            //ChangeType 无法将其他类型转换成枚举类型，请使用c#强制转换运算
            try
            {
                Console.WriteLine("{0}", Convert.ChangeType(number, typeof(Continent)));
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Cannot convert a Double to a Continent");
            }
            Console.WriteLine("{0}", (Continent)number);
            var d = Convert.ChangeType("2024-09-02", typeof(DateTime));
            Console.WriteLine(d);
        }

        public enum Continent
        {
            Africa, Antarctica, Asia, Australia, Europe,
            NorthAmerica, SouthAmerica
        };
    }
}
