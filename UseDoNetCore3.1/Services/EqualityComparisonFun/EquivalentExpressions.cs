
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper.Features;
using System.Linq;
using AutoMapper.Internal;
using System.Reflection;
using System.Collections.Concurrent;
using AutoMapper.Collection.Runtime;
using AutoMapper.Collection.Configuration;
using AutoMapper.EquivalencyExpression;

namespace VOL.Core.Extensions.AutofacManager.EqualityComparisonFun
{
    public static class EquivalentExpressions
    {
        /// <summary>
        /// Make Comparison between <typeparamref name="TSource"/> and <typeparamref name="TDestination"/>
        /// </summary>
        /// <typeparam name="TSource">Compared type</typeparam>
        /// <typeparam name="TDestination">Type being compared to</typeparam>
        /// <param name="mappingExpression">Base Mapping Expression</param>
        /// <param name="EquivalentExpression">Equivalent Expression between <typeparamref name="TSource"/> and <typeparamref name="TDestination"/></param>
        /// <returns></returns>
        public static IMappingExpression<TSource, TDestination> EqualityComparison<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression, Expression<Func<TSource, TDestination, bool>> EquivalentExpression)
        {
            mappingExpression.Features.Set(new CollectionMappingExpressionFeatureNew<TSource, TDestination>(EquivalentExpression));
            return mappingExpression;
        }
    }

    public class CollectionMappingExpressionFeatureNew<TSource, TDestination> : IMappingFeature
    {
        private readonly Expression<Func<TSource, TDestination, bool>> _expression;

        public CollectionMappingExpressionFeatureNew(Expression<Func<TSource, TDestination, bool>> expression)
        {
            _expression = expression;
        }

        public void Configure(TypeMap typeMap)
        {
            var equivalentExpression = new EquivalentExpressionNew<TSource, TDestination>(_expression);
            typeMap.Features.Set(new CollectionMappingFeature(equivalentExpression));
        }

        public IMappingFeature Reverse()
        {
            var reverseExpression = Expression.Lambda<Func<TDestination, TSource, bool>>
                (_expression.Body, _expression.Parameters[1], _expression.Parameters[0]);
            return new CollectionMappingExpressionFeature<TDestination, TSource>(reverseExpression);
        }
    }

    public class EquivalentExpressionNew<TSource, TDestination> : IEquivalentComparer<TSource, TDestination>
    {
        private readonly Expression<Func<TSource, TDestination, bool>> _equivalentExpression;
        private readonly Func<TSource, TDestination, bool> _equivalentFunc;
        private readonly Func<TSource, int> _sourceHashCodeFunc;
        private readonly Func<TDestination, int> _destinationHashCodeFunc;

        public EquivalentExpressionNew(Expression<Func<TSource, TDestination, bool>> equivalentExpression)
        {
            _equivalentExpression = equivalentExpression;
            _equivalentFunc = _equivalentExpression.Compile();

            var sourceParameter = equivalentExpression.Parameters[0];
            var destinationParameter = equivalentExpression.Parameters[1];

            var members = HashableExpressionsVisitorNew.Expand(sourceParameter, destinationParameter, equivalentExpression);

            _sourceHashCodeFunc = GetHashCodeExpression<TSource>(members.Item1,sourceParameter).Compile();
            _destinationHashCodeFunc =GetHashCodeExpression<TDestination>(members.Item2,destinationParameter).Compile();
        }

        public bool IsEquivalent(object source, object destination)
        {

            if (source == null && destination == null)
            {
                return true;
            }

            if (source == null || destination == null)
            {
                return false;
            }

            if (!(source is TSource src) || !(destination is TDestination dest))
            {
                return false;
            }

            return _equivalentFunc(src, dest);
        }

        public Expression<Func<TDestination, bool>> ToSingleSourceExpression(TSource source)
        {
            if (source == null)
                throw new Exception("Invalid somehow");

            var expression = new ParametersToConstantVisitorNew<TSource>(source).Visit(_equivalentExpression) as LambdaExpression;
            return Expression.Lambda<Func<TDestination, bool>>(expression.Body, _equivalentExpression.Parameters[1]);
        }

        public int GetHashCode(object obj)
        {
            if (obj is TSource src)
                return _sourceHashCodeFunc(src);
            if (obj is TDestination dest)
                return _destinationHashCodeFunc(dest);
            return default(int);
        }

        public static Expression<Func<T, int>> GetHashCodeExpression<T>(List<Expression> members, ParameterExpression sourceParam)
        {
            var hashMultiply = Expression.Constant(397L);

            var hashVariable = Expression.Variable(typeof(long), "hashCode");
            var returnTarget = Expression.Label(typeof(int));
            var returnExpression = Expression.Return(returnTarget, Expression.Convert(hashVariable, typeof(int)), typeof(int));
            var returnLabel = Expression.Label(returnTarget, Expression.Constant(-1));

            var expressions = new List<Expression>();
            foreach (var member in members)
            {
                // Call the GetHashCode method
                var hasCodeExpression = Expression.Convert(Expression.Call(member, GetDeclaredMethod(member.Type,nameof(GetHashCode))), typeof(long));

                // return (((object)x) == null ? 0 : x.GetHashCode())
                var hashCodeReturnTarget = Expression.Label(typeof(long));
                var hashCode = Expression.Block(
                    Expression.IfThenElse(
                        Expression.ReferenceEqual(Expression.Convert(member, typeof(object)), Expression.Constant(null)),
                        Expression.Return(hashCodeReturnTarget, Expression.Constant(0L, typeof(long))),
                        Expression.Return(hashCodeReturnTarget, hasCodeExpression)),
                    Expression.Label(hashCodeReturnTarget, Expression.Constant(0L, typeof(long))));

                if (expressions.Count == 0)
                {
                    expressions.Add(Expression.Assign(hashVariable, hashCode));
                }
                else
                {
                    var oldHashMultiplied = Expression.Multiply(hashVariable, hashMultiply);
                    var xOrHash = Expression.ExclusiveOr(oldHashMultiplied, hashCode);
                    expressions.Add(Expression.Assign(hashVariable, xOrHash));
                }
            }

            expressions.Add(returnExpression);
            expressions.Add(returnLabel);

            var resutltBlock = Expression.Block(new[] { hashVariable }, expressions);

            return Expression.Lambda<Func<T, int>>(resutltBlock, sourceParam);
        }       
        public static MethodInfo GetDeclaredMethod(Type type, string name)
        {
            return type.GetRuntimeMethods().FirstOrDefault(mi => mi.Name == name);
        }
    }

    internal class HashableExpressionsVisitorNew : ExpressionVisitor
    {
        private readonly List<Expression> _destinationMembers = new List<Expression>();
        private readonly ParameterExpression _destinationParameter;
        private readonly List<Expression> _sourceMembers = new List<Expression>();
        private readonly ParameterExpression _sourceParameter;

        private readonly ParameterFinderVisitorNew _paramFinder = new ParameterFinderVisitorNew();

        internal HashableExpressionsVisitorNew(ParameterExpression sourceParameter, ParameterExpression destinationParameter)
        {
            _sourceParameter = sourceParameter;
            _destinationParameter = destinationParameter;
        }

        public static Tuple<List<Expression>, List<Expression>> Expand(ParameterExpression sourceParameter, ParameterExpression destinationParameter, Expression expression)
        {
            var visitor = new HashableExpressionsVisitorNew(sourceParameter, destinationParameter);
            visitor.Visit(expression);
            return Tuple.Create(visitor._sourceMembers, visitor._destinationMembers);
        }

        protected override Expression VisitConditional(ConditionalExpression node)
        {
            return node;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    VisitCompare(node.Left, node.Right);
                    break;
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    return base.VisitBinary(node);
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return node; // Maybe compare 0r's for expression matching on side
            }

            return node;
        }

        private void VisitCompare(Expression leftNode, Expression rightNode)
        {
            _paramFinder.Visit(leftNode);
            var left = _paramFinder.Parameters;
            _paramFinder.Visit(rightNode);
            var right = _paramFinder.Parameters;

            if (left.All(p => p == _destinationParameter) && right.All(p => p == _sourceParameter))
            {
                _sourceMembers.Add(rightNode);
                _destinationMembers.Add(leftNode);
            }
            if (left.All(p => p == _sourceParameter) && right.All(p => p == _destinationParameter))
            {
                _sourceMembers.Add(leftNode);
                _destinationMembers.Add(rightNode);
            }
        }
    }

    internal class ParameterFinderVisitorNew : ExpressionVisitor
    {
        public IList<ParameterExpression> Parameters { get; private set; }

        public override Expression Visit(Expression node)
        {
            Parameters = new List<ParameterExpression>();
            return base.Visit(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            Parameters.Add(node);
            return node;
        }
    }

    internal class ParametersToConstantVisitorNew<T> : ExpressionVisitor
    {
        private readonly T _value;
        public ParametersToConstantVisitorNew(T value)
        {
            _value = value;
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node;
        }
        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member is PropertyInfo && node.Member.DeclaringType.GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                var memberExpression = Expression.Constant(node.Member.GetMemberValue(_value));
                return memberExpression;
            }
            return base.VisitMember(node);
        }
    }  
}
