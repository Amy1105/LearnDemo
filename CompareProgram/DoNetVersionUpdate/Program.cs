// See https://aka.ms/new-console-template for more information
using DoNetVersionUpdate;
using DoNetVersionUpdate.DoNet5Update;
using DoNetVersionUpdate.enums;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/*
 C#13 
        新的转义序列
        方法组自然类型
        隐式索引访问
        另请参阅
 * 
 C#12
        主构造函数
        集合表达式
        ref readonly 参数
        默认 Lambda 参数
        任何类型的别名
        内联数组
        Experimental 属性
        拦截器
 * 
 C#11 
        泛型属性
        泛型数学支持
        数值 IntPtr 和 UIntPtr
        字符串内插中的换行符
        列表模式
        改进了方法组向委托的转换
        原始字符串文本
        自动默认结构
        常量 string 上的模式匹配 Span<char> 或 ReadOnlySpan<char>
        扩展的 nameof 范围
        UTF-8 字符串字面量
        必需的成员
        ref 字段和 ref scoped 变量
        文件本地类型
 * 
 C#10
        记录结构
        结构类型的改进
        内插字符串处理程序
        全局 using 指令
        文件范围的命名空间声明
        扩展属性模式
        Lambda 表达式改进
        常数内插字符串
        记录类型可以密封 ToString
        在同一析构中进行赋值和声明
        改进型明确赋值
        允许在方法上使用 AsyncMethodBuilder 特性
        CallerArgumentExpression 属性诊断
        增强型 #line pragma
 * 
 C#9
        记录
        仅限 Init 的资源库
        顶级语句
        模式匹配增强：关系模式和逻辑模式
        性能和互操作性
            本机大小的整数
            函数指针
            禁止发出 localsinit 标志
            模块初始值设定项
            分部方法的新功能
        调整和完成功能
            目标类型的 new 表达式
            static 匿名函数
            目标类型的条件表达式
            协变返回类型
            扩展 GetEnumerator 支持 foreach 循环
            Lambda 弃元参数
            本地函数的属性
 */

//withDemo.Method2();


//.net9  c# 13
//.net8  c#12
//.net7  c#11
//.net 6 c#10
//.net 5 c#9
//.net core3.1  C#8

//recordDemo.Method();

//recordDemo.Method3();
//recordDemo.Method5();
//recordDemo.Method6();
//recordDemo.Method7();

//recordDemo.Method8();
//recordDemo.Method9();
//recordDemo.Method10();

//recordDemo.Method11();
//recordDemo.Method12();
//recordDemo.Method13();
//recordDemo.Method14();

//initDemo.Method();

//modeDemo.Method();

//modeDemo.Method2();
//modeDemo.Method3();
//modeDemo.GetGroupTicketPrice(4);
//modeDemo.Method4();


//modeDemo.Method5();
//modeDemo.Method6();
//modeDemo.Method7();
//modeDemo.Method8();
//modeDemo.Method9();
//modeDemo.Method();
//modeDemo.Method();

//D d=new D();
//d.M2();
//d.M3();


var values=Enum.GetValues(typeof(OUT_ORDER_STATUS));


var names = Enum.GetNames(typeof(OUT_ORDER_STATUS));
foreach (var item in names)
{
    Console.WriteLine(item);
}

Dictionary<int, string> pairs = new Dictionary<int, string>();
foreach (OUT_ORDER_STATUS item in values)
{
    pairs.Add(item.GetValue(), item.GetDescription());
}

// 遍历只读集合
foreach (var pair in pairs)
{
    Console.WriteLine($"Key: {pair.Key}, Value: {pair.Value}");
}



Console.WriteLine("Done.");