using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.Expressions
{
    /// <summary>
    /// 使用Expression代替反射
    /// </summary>
    public class ExpressionDemo
    {
        //public void ReflectionMethod()
        //{
        //    Type type = typeof(MyClass);
        //    MethodInfo method = type.GetMethod("MyMethod");
        //    object result = method.Invoke(instance, null);
        //}

        //public void ExpressionMethod()
        //{
        //    ParameterExpression param = Expression.Parameter(typeof(MyClass), "p");
        //    MemberExpression prop = Expression.Property(param, "MyProperty");
        //    Expression<Func<MyClass, string>> lambda = Expression.Lambda<Func<MyClass, string>>(prop, param);

        //    Func<MyClass, string> compiled = lambda.Compile();
        //    string value = compiled(instance);
        //}


        public static void Test()
        {
            var myClass = new MyClass();

            // 动态调用 Print 方法
            CallMethod(myClass, "Print", "Hello, Expression!");

            //// 动态调用 Add 方法
            //CallMethod<int>(myClass, "Add", 3, 5);
            //Console.WriteLine($"Add Result: {result}");
        }

        public static void CallMethod<TInstance>(TInstance instance, string methodName, params object[] parameters)
        {
            var methodInfo = typeof(TInstance).GetMethod(methodName, parameters.Select(p => p.GetType()).ToArray());
            if (methodInfo == null)
            {
                throw new InvalidOperationException($"Method '{methodName}' not found.");
            }

            // 创建参数表达式
            var instanceExpr = Expression.Constant(instance);
            var parameterExprs = parameters.Select(p => Expression.Constant(p));

            // 创建方法调用表达式
            var methodCallExpr = Expression.Call(instanceExpr, methodInfo, parameterExprs);

            // 将表达式编译为委托并执行
            var action = Expression.Lambda<Action>(methodCallExpr).Compile();
            action();
        }

        public static TResult CallMethod<TInstance, TResult>(TInstance instance, string methodName, params object[] parameters)
        {
            var methodInfo = typeof(TInstance).GetMethod(methodName, parameters.Select(p => p.GetType()).ToArray());
            if (methodInfo == null)
            {
                throw new InvalidOperationException($"Method '{methodName}' not found.");
            }

            // 创建参数表达式
            var instanceExpr = Expression.Constant(instance);
            var parameterExprs = parameters.Select(p => Expression.Constant(p));

            // 创建方法调用表达式
            var methodCallExpr = Expression.Call(instanceExpr, methodInfo, parameterExprs);

            // 将表达式编译为委托并执行
            var func = Expression.Lambda<Func<TResult>>(methodCallExpr).Compile();
            return func();
        }

    }

}
