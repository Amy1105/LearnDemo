using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNetVersionUpdate.DoNet6Update
{
    /// <summary>
    /// recode class  引用类型
    /// </summary>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    public record Person(string FirstName, string LastName);

    public record Person2
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
    };
   /// <summary>
   /// recode struct 值类型
   /// </summary>
   /// <param name="X"></param>
   /// <param name="Y"></param>
   /// <param name="Z"></param>
    public readonly record struct Point(double X, double Y, double Z);


    public record struct Point2
    {
        public double X { get; init; }
        public double Y { get; init; }
        public double Z { get; init; }
    }
    public class C_Demo
    {
//        记录结构


//结构类型的改进
//内插字符串处理程序
//global using 指令
//文件范围的命名空间声明
//扩展属性模式
//对 Lambda 表达式的改进
//可使用 const 内插字符串
//记录类型可密封 ToString()
//改进型明确赋值
//在同一析构中可同时进行赋值和声明
//可在方法上使用 AsyncMethodBuilder 属性
//CallerArgumentExpression 属性
//增强的
//#line pragma
//警告波 6

    }
}
