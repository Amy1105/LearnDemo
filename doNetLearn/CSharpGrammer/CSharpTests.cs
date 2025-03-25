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

        public void Method9()
        {

        }

        public void Method10()
        {

        }

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


        public void Method12()
        {
            //主构造函数
            //集合表达式
            //ref readonly 个参数
            //默认 lambda 参数
            //任何类型的别名
            //内联数组
            //Experimental 属性
            //拦截器
        }

        /// <summary>
        /// .net 9    c#13
        /// </summary>
        public void Method13()
        {
            //params 集合
            //新锁定对象
            //新的转义序列
            //方法组自然类型
            //隐式索引访问
            //迭代器和 async 方法中的 ref和 unsafe
            //allows ref struct
            //ref struct 接口
            //更多部分成员
            //重载解析优先级
            //field 关键字
        }

    }
}
