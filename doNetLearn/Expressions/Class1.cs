using System.Linq.Expressions;
using System.Reflection;
using static doNetLearn.Expressions.ReflectionDemo;

namespace doNetLearn.Expressions
{

    public class ReflectionCache
    {
        private static readonly Dictionary<string, Type> _typeCache = new Dictionary<string, Type>();
        private static readonly Dictionary<string, MethodInfo> _methodCache = new Dictionary<string, MethodInfo>();
        private static readonly Dictionary<string, PropertyInfo> _propertyCache = new Dictionary<string, PropertyInfo>();

        public static Type GetType(string typeName)
        {
            if (!_typeCache.TryGetValue(typeName, out var type))
            {
                type = Type.GetType(typeName);
                _typeCache[typeName] = type;
            }
            return type;
        }

        public static MethodInfo GetMethod(Type type, string methodName, Type[] parameterTypes)
        {
            string key = $"{type.FullName}.{methodName}";
            if (!_methodCache.TryGetValue(key, out var methodInfo))
            {
                methodInfo = type.GetMethod(methodName, parameterTypes);
                _methodCache[key] = methodInfo;
            }
            return methodInfo;
        }

        public static PropertyInfo GetProperty(Type type, string propertyName)
        {
            string key = $"{type.FullName}.{propertyName}";
            if (!_propertyCache.TryGetValue(key, out var propertyInfo))
            {
                propertyInfo = type.GetProperty(propertyName);
                _propertyCache[key] = propertyInfo;
            }
            return propertyInfo;
        }
    }


    public class ExpressionCache
    {
        private static readonly Dictionary<string, Delegate> _cachedDelegates = new Dictionary<string, Delegate>();

        public static void Main(string[] args)
        {
            var calculator = new Calculator();

            // 第一次调用，编译并缓存委托
            CallMethodCached(calculator, "Print", "Hello, Cached Expression!");

            // 第二次调用，直接使用缓存的委托
            CallMethodCached(calculator, "Print", "Cached call!");
        }

        public static void CallMethodCached(object obj, string methodName, params object[] parameters)
        {
            var cacheKey = $"{obj.GetType().FullName}.{methodName}";
            if (!_cachedDelegates.TryGetValue(cacheKey, out var action))
            {
                var methodInfo = obj.GetType().GetMethod(methodName, parameters.Select(p => p.GetType()).ToArray());
                if (methodInfo == null)
                {
                    throw new InvalidOperationException($"Method '{methodName}' not found.");
                }

                var instanceExpr = Expression.Constant(obj);
                var parameterExprs = parameters.Select(p => Expression.Constant(p));
                var methodCallExpr = Expression.Call(instanceExpr, methodInfo, parameterExprs);

                action = Expression.Lambda<Action>(methodCallExpr).Compile();
                _cachedDelegates[cacheKey] = action;
            }
            ((Action)action)();
        }
    }
}