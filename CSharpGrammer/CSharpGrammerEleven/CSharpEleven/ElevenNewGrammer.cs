using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGrammer.CSharpEight
{
    /// <summary>
    /// C# 11 feature
    /// </summary>
    public class ElevenNewGrammer
    {
        //1.泛型属性
        //2.泛型数学支持
        //3.数值 IntPtr 和 UIntPtr
        //现在 nint 和 nuint 类型的别名分别为 System.IntPtr 和 System.UIntPtr。
        
        /// <summary>
        /// 4.字符串内插中的换行符
        /// </summary>
        public void GetString()
        {
            string a = "10";
            string b = "12";
            string c = "13";
            string d = "35";
            var v = $"Count is\t: {a+" "+b+
                c+"="+d}.";
        }

//5.列表模式

//6.改进了方法组向委托的转换
//7.原始字符串文本
//8.自动默认结构
//9.常量 string 上的模式匹配 Span<char> 或 ReadOnlySpan<char>
//10.扩展的 nameof 范围
//11.UTF-8 字符串字面量
//12.必需的成员
//13.ref 字段和 ref scoped 变量
//14.文件本地类型

    }

    // :

    /// <summary>
    /// 泛型属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericAttribute<T> : Attribute { }

   
    public class GenericType<T>
    {
        [GenericAttribute<string>()]
        public string Method() => default;

        //[GenericAttribute<T>()] // 不允许！泛型属性必须是完全构造的类型。
        //public string MethodNo() => default;
    }

   
}
