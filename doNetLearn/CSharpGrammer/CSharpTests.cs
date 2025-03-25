using doNetLearn.ThreadDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.CSharpGrammer
{
    internal class CSharpTests
    {
        public void Method11()
        {
            //1.原始字符串字面量
            CSharp11.Method1();

            //字符串内插表达式中的换行符
            CSharp11.Method13();

            //泛型数学支持


            //泛型属性


            //UTF-8 字符串字面量
            CSharp11.Method2();


            //必需的成员  required
            CSharp11.Method3();


            //自动默认结构
            //// 在C# 11中，无需手动设置默认值：
            CSharp11.Point p = new(); // X和Y被初始化为0。
                              
            //扩展的 nameof 范围
            CSharp11.Method5();

            //数值 IntPtr
            CSharp11.Method6();

            //ref 字段和 scoped ref
            // 结构中可以使用ref，减少内存分配
           

            //常量 string 上的模式匹配 Span<char>
            CSharp11.Method4();


            //列表模式


            //文件本地类型


            //改进了方法组向委托的转换
            //CSharp11.EventHandlerExample

            //警告波 7
        }
    }
}
