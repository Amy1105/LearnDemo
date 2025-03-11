using System.Linq.Expressions;
using static doNetLearn.Expressions.ReflectionDemo;

namespace doNetLearn.Expressions
{
    public class ReflectionDemo
    {
        /// <summary>
        /// 反射使用场景：
        ///  1.动态创建对象实例。
        ///  2.动态获取对象的属性或字段值。
        ///  3.动态调用对象的方法。
        ///  
        /// 反射的性能通常较低，因为它在运行时需要解析类型信息。为了优化性能，可以考虑以下方法:
        /// 1.缓存反射结果:将反射获取的类型、方法、属性等信息缓存起来，避免重复解析
        /// 2.使用 Expression 表达式树:将反射操作转换为 Expression 表达式树，并编译为委托，以提高性能
        /// </summary>
        public void ReflectionMethod()
        {
            //1.动态创建对象实例
            ////类的完全限定名称
            string className = "MyClass";

            ////根据类的完全限定名称获取类型
            Type? type = Type.GetType(className);  
            if (type == null)
            {
                Console.WriteLine("Type not found.");
                return;
            }

            ////创建对象实例
            object? instance = Activator.CreateInstance(type);  //创建该类型的实例

            ////调用方法
            var methodInfo = type.GetMethod("Print");  //调用实例的方法
            methodInfo?.Invoke(instance, null);

            //2.动态获取对象的属性或字段值
            var person = new Person { Name = "Alice", Age = 30 };

            ////获取属性值
            string name = string.Empty;
            var nameValue = GetPropertyValue(person, "Name");
            if (nameValue != null)
            {
                name = nameValue! as string;
            }
            int age =default;

            var ageValue = GetPropertyValue(person, "Age");
            if (ageValue != null)
            {
                age = (int)ageValue;
            }

            Console.WriteLine($"Name: {name}, Age: {age}");

            //3. 动态调用对象的方法
            var calculator = new Calculator();
            CallMethod(calculator, "Print", "Hello, Reflection!");

            // 调用有返回值的方法
            var result = CallMethod(calculator, "Add", 3, 5);
            if (result != null)
            {               
                Console.WriteLine($"Add Result: {(int)result}");
            }           
        }

        public static object? GetPropertyValue(object obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName); //获取对象的属性信息
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Property '{propertyName}' not found.");
            }

            return propertyInfo.GetValue(obj); //获取属性的值
        }

        public static object? CallMethod(object obj, string methodName, params object[] parameters)
        {
            //获取对象的方法信息
            var methodInfo = obj.GetType().GetMethod(methodName, parameters.Select(p => p.GetType()).ToArray());
            if (methodInfo == null)
            {
                throw new InvalidOperationException($"Method '{methodName}' not found.");
            }
            //调用方法并传入参数
            return methodInfo.Invoke(obj, parameters);
        }
     
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public void Print(string message)
        {
            Console.WriteLine($"Message: {message}");
        }
    }

    public class MyClass
    {
        public void Print()
        {
            Console.WriteLine("MyClass instance created!");
        }
    }

    /// <summary>
    /// 使用Expression代替反射
    /// 
    /// Expression 表达式树的性能优势在于它可以编译为委托并缓存。因此，在实际使用中，可以将生成的委托缓存起来，避免重复编译。
    /// </summary>
    public class ExpressionDemo
    {
        public void ExpressionMethod()
        {
            //1. 使用 Expression 动态创建对象

            Type type = typeof(MyClass);

            // 使用 Expression 创建对象实例
            var instance = CreateInstance(type);

            // 调用方法
            var methodInfo = type.GetMethod("Print");
            methodInfo?.Invoke(instance, null);


            //2. 使用 Expression 动态获取对象的属性值
            var person = new Person { Name = "Alice", Age = 30 };

            // 动态获取属性值
            string name = GetPropertyValue<string>(person, "Name");
            int age = GetPropertyValue<int>(person, "Age");

            Console.WriteLine($"Name: {name}, Age: {age}");


            //3.使用 Expression 动态调用对象的方法

            var calculator = new Calculator();

            // 调用无返回值的方法
            CallMethod(calculator, "Print", "Hello, Expression!");

            // 调用有返回值的方法
            int result = CallMethod<int>(calculator, "Add", 3, 5);
            Console.WriteLine($"Add Result: {result}");
        }
        public static object CreateInstance(Type type)
        {
            // 创建 NewExpression 表示调用构造函数
            var newExpr = Expression.New(type);  //创建一个表示调用无参数构造函数的 NewExpression

            // 将 NewExpression 转换为 LambdaExpression
            var lambdaExpr = Expression.Lambda<Func<object>>(newExpr); //将 NewExpression 包装为 LambdaExpression 

            // 编译为委托并执行
            var func = lambdaExpr.Compile(); //将表达式编译为委托并执行
            return func();
        }

        public static TValue GetPropertyValue<TValue>(object obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new InvalidOperationException($"Property '{propertyName}' not found.");
            }

            // 创建一个表示对象的常量表达式
            var instanceExpr = Expression.Constant(obj);

            // 创建一个表示属性访问的表达式
            var propertyExpr = Expression.Property(instanceExpr, propertyInfo);

            // 将属性访问表达式包装为 LambdaExpression。
            var func = Expression.Lambda<Func<TValue>>(propertyExpr).Compile();
            return func();
        }

        public static void CallMethod(object obj, string methodName, params object[] parameters)
        {
            var methodInfo = obj.GetType().GetMethod(methodName, parameters.Select(p => p.GetType()).ToArray());
            if (methodInfo == null)
            {
                throw new InvalidOperationException($"Method '{methodName}' not found.");
            }

            // 创建参数表达式
            var instanceExpr = Expression.Constant(obj);
            var parameterExprs = parameters.Select(p => Expression.Constant(p));

            // 创建方法调用表达式
            var methodCallExpr = Expression.Call(instanceExpr, methodInfo, parameterExprs);

            // 将表达式编译为委托并执行
            var action = Expression.Lambda<Action>(methodCallExpr).Compile();
            action();
        }

        public static TResult CallMethod<TResult>(object obj, string methodName, params object[] parameters)
        {
            var methodInfo = obj.GetType().GetMethod(methodName, parameters.Select(p => p.GetType()).ToArray());
            if (methodInfo == null)
            {
                throw new InvalidOperationException($"Method '{methodName}' not found.");
            }

            // 创建参数表达式
            var instanceExpr = Expression.Constant(obj);
            var parameterExprs = parameters.Select(p => Expression.Constant(p));

            // 创建方法调用表达式
            var methodCallExpr = Expression.Call(instanceExpr, methodInfo, parameterExprs);

            // 将表达式编译为委托并执行
            var func = Expression.Lambda<Func<TResult>>(methodCallExpr).Compile();
            return func();
        }
    }
}
