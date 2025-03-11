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
