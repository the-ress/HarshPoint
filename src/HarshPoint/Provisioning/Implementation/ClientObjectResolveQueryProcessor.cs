﻿using Microsoft.SharePoint.Client;
using System;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace HarshPoint.Provisioning.Implementation
{
    internal sealed class ClientObjectResolveQueryProcessor
    {
        private readonly IImmutableDictionary<Type, IImmutableList<Expression>> _retrievals;

        public ClientObjectResolveQueryProcessor(IImmutableDictionary<Type, IImmutableList<Expression>> retrievals)
        {
            if (retrievals == null)
            {
                throw Error.ArgumentNull(nameof(retrievals));
            }

            _retrievals = retrievals;
        }

        public Expression AddContextRetrievals(Expression expression)
        {
            if (expression == null)
            {
                throw Error.ArgumentNull(nameof(expression));
            }

            var visitor = new Visitor(_retrievals);
            var result = visitor.Visit(expression);
            return result;
        }

        private sealed class Visitor : ExpressionVisitor
        {
            public Visitor(IImmutableDictionary<Type, IImmutableList<Expression>> retrievals)
            {
                Retrievals = retrievals;
            }

            public IImmutableDictionary<Type, IImmutableList<Expression>> Retrievals
            {
                get;
                private set;
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                if (node == null)
                {
                    throw Error.ArgumentNull(nameof(node));
                }

                if (!IsIncludeOrIncludeWithDefaultProperties(node))
                {
                    return base.VisitMethodCall(node);
                }

                var retrievedType = node.Method.GetGenericArguments().Single();
                var retrievals = node.Arguments[1] as NewArrayExpression;

                if (retrievals == null)
                {
                    throw Error.ArgumentOutOfRangeFormat(
                       nameof(node),
                       SR.ClientObjectResolveQueryProcessor_IncludeArgNotArray,
                       node
                   );
                }

                var retrievalsCombined = new ReadOnlyCollection<Expression>(
                    retrievals.Expressions
                    .Concat(
                        Retrievals.GetValueOrDefault(
                            retrievedType, 
                            ImmutableArray<Expression>.Empty
                        )
                    )
                    .ToArray()
                );

                if (!retrievalsCombined.Any())
                {
                    return Visit(node.Arguments[0]);
                }

                return Expression.Call(
                    null,
                    node.Method,
                    Visit(node.Arguments[0]),
                    Expression.NewArrayInit(
                        typeof(Expression<>).MakeGenericType(
                            typeof(Func<,>).MakeGenericType(
                                retrievedType,
                                typeof(Object)
                            )
                        ),
                        Visit(retrievalsCombined)
                    )
                );
            }

            private static Boolean IsIncludeOrIncludeWithDefaultProperties(MethodCallExpression node)
            {
                if (node == null)
                {
                    return false;
                }

                if (!node.Method.DeclaringType.Equals(typeof(ClientObjectQueryableExtension)))
                {
                    return false;
                }

                if (!node.Method.Name.Equals("Include") &&
                    !node.Method.Name.Equals("IncludeWithDefaultProperties"))
                {
                    return false;
                }

                if (node.Arguments.Count != 2)
                {
                    return false;
                }

                if (!node.Method.IsGenericMethod)
                {
                    return false;
                }

                var genericArguments = node.Method.GetGenericArguments();

                if (genericArguments.Length != 1)
                {
                    return false;
                }

                return true;
            }
        }
    }
}