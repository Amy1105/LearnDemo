namespace doNetLearn.DesignPatterns
{
    internal class DesignPatternsMethod
    {
        public void execute()
        {
            CoRPattern.execute();

            DecoratorPattern decoratorPattern = new DecoratorPattern();
            decoratorPattern.execute();

            StatePatterns.StatePatterns statePatterns = new StatePatterns.StatePatterns();
            statePatterns.execute();
        }
    }
}
