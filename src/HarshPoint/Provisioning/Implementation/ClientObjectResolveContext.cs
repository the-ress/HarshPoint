﻿using Microsoft.SharePoint.Client;
using System;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;

namespace HarshPoint.Provisioning.Implementation
{
    [Obsolete]
    internal sealed class ClientObjectResolveContext : ResolveContext<HarshProvisionerContext>
    {
        private ImmutableDictionary<Type, IImmutableList<Expression>> _retrievals =
            ImmutableDictionary<Type, IImmutableList<Expression>>.Empty;

        public void Include<T>(params Expression<Func<T, Object>>[] retrievals)
            where T : ClientObject
        {
            if (retrievals == null)
            {
                throw Error.ArgumentNull(nameof(retrievals));
            }

            _retrievals = _retrievals.SetItem(
                typeof(T),
                GetRetrievals(typeof(T)).AddRange(retrievals)
            );
        }

        public Expression<Func<T, Object>>[] GetRetrievals<T>()
            where T : ClientObject
        {
            return GetRetrievals(typeof(T))
                .Cast<Expression<Func<T, Object>>>()
                .ToArray();
        }

        public IImmutableList<Expression> GetRetrievals(Type type)
        {
            if (type == null)
            {
                throw Error.ArgumentNull(nameof(type));
            }

            return _retrievals.GetValueOrDefault(type, EmptyExpressionList);
        }
        
        public IImmutableDictionary<Type, IImmutableList<Expression>> Retrievals
            => _retrievals;

        private static readonly ImmutableList<Expression> EmptyExpressionList =
            ImmutableList<Expression>.Empty;
    }
}
