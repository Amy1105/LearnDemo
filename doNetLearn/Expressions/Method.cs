using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doNetLearn.Expressions
{
    internal class ExpressionsMethod
    {
        public void Execute()
        {
            ReflectionDemo reflectionDemo = new ReflectionDemo();
            reflectionDemo.ReflectionMethod();

            ExpressionDemo expressionDemo = new ExpressionDemo();
            expressionDemo.ExpressionMethod();

        }
    }
}
