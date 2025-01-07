using Microsoft.CodeAnalysis;
using System;
using System.Diagnostics;

namespace SG1
{
    //[Generator]
    public class AddAMenthodGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("MyClass", "public class MyClass{public static void Test(){System.Console.WriteLine(666);}}");
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            Debugger.Launch();//可以调试Myclass的代码
        }
    }
}
